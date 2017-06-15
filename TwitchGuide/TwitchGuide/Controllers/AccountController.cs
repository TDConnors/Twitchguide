using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TwitchGuide.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using TwitchGuide.DAL;
using System.Data.Entity;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TwitchGuide.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private TwitchContext db = new TwitchContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            if (Request.Cookies["Identity.External"] != null)
            {
                return RedirectToAction(nameof(ExternalLoginCallback), returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl, string code)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            var user = await UserManager.FindAsync(loginInfo.Login);

            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    await StoreTwitchToken(await UserManager.FindAsync(loginInfo.Login));//stores the users twitch token
                    return RedirectToAction("addTokenToDB", "Account");
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "LoginSuccess");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await StoreTwitchToken(user);
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("addTokenToDB", "Account");
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        private async Task StoreTwitchToken(ApplicationUser user)
        {
            var claimsIdentity = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
            if (claimsIdentity != null)
            {
                // Retrieve the existing claims for the user and add the TwitchAccessTokenClaim
                var currentClaims = await UserManager.GetClaimsAsync(user.Id);
                var twitchToken = claimsIdentity.Claims.Where(a => a.Type.Contains("twitch:access_token")).FirstOrDefault();
                if (twitchToken != null)
                {
                    if (currentClaims.Count(a => a.Type.Contains("twitch:access_token")) > 0)
                    {
                        await UserManager.RemoveClaimAsync(user.Id, twitchToken);
                    }

                    await UserManager.AddClaimAsync(user.Id, twitchToken);
                }
            }
        }

        [Authorize]
        public ActionResult addTokenToDB()
        {
            //store into our own Users table
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.Claims.Where(a => a.Type.Contains("twitch:access_token")).FirstOrDefault();

            if (token == null)
            {
                return RedirectToAction("TokenNULL", "Home");
            }

            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            var ourUser = db.Users.Where(p => p.Username == currentUser.UserName).FirstOrDefault();

            if (ourUser == null)
            {
                User newUser = new Models.User
                {
                    Username = currentUser.UserName
                };
                db.Users.Add(newUser);
                db.SaveChanges();

                ourUser = db.Users.Where(p => p.Username == currentUser.UserName).FirstOrDefault();
            }

            ourUser.AuthToken = token.Value;

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/kraken/user");
            myRequest.Method = "GET";
            myRequest.ContentType = "application/json; charset=utf-8";
            myRequest.Accept = "application / vnd.twitchtv.v5 + json";
            myRequest.Headers["Client-ID"] = "kx6k6d64t0ok27s5sfyo1w5n1q3dn6";
            myRequest.Headers["Authorization"] = "OAuth " + ourUser.AuthToken;
            //Get Response
            HttpWebResponse response = myRequest.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string userData = reader.ReadToEnd();
                var json = JsonConvert.DeserializeObject<dynamic>(userData);

                ourUser.Username = json.display_name;
                ourUser.Avatar = json.logo;
                db.Entry(ourUser).State = EntityState.Modified;
                db.SaveChanges();
                currentUser.UserName = json.display_name;
                UserManager.Update(currentUser);


                ourUser.TwitchID = Convert.ToInt32(json._id);
                db.Entry(ourUser).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("LoginSuccess", "Home");
            }

        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("LoginSuccess", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}