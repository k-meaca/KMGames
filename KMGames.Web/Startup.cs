using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(KMGames.Web.Startup))]
namespace KMGames.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
