using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shorten_Urls.Startup))]
namespace Shorten_Urls
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
