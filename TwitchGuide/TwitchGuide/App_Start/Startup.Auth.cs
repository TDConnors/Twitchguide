﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TwitchGuide.Models;
using Owin.Security.Providers.Twitch;
using System.Configuration;
using System.Security.Claims;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace TwitchGuide
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            /*
             * Twitch sign-ins use /signin-Twitch as the URL for authentication
             *
             */

            ////Simple Twitch Sign-in
            //app.UseTwitchAuthentication("kx6k6d64t0ok27s5sfyo1w5n1q3dn6", ConfigurationManager.AppSettings["TwitchSecret"]);

            //More complex Twitch Sign-in
            var opt = new TwitchAuthenticationOptions()
            {
                ClientId = "kx6k6d64t0ok27s5sfyo1w5n1q3dn6",
                ClientSecret = ConfigurationManager.AppSettings["TwitchSecret"],
                Provider = new TwitchAuthenticationProvider()
                {
                    //OnAuthenticated = async z =>
                    OnAuthenticated = context =>
                    {
                        //Getting the twitch users picture
                        //z.Identity.AddClaim(new Claim("Picture", z.User.GetValue("logo").ToString()));

                        context.Identity.AddClaim(new Claim("urn:twitch:access_token", context.AccessToken));
                        return Task.FromResult(0);
                    }
                },
                Scope = { "user_read", "channel_read", "channel_editor", "user_follows_edit" }
            };
            app.UseTwitchAuthentication(opt);

        }
    }
}