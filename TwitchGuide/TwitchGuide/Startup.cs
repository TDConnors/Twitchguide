using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitchGuide.Startup))]
namespace TwitchGuide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
