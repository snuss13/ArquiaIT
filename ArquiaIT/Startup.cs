using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArquiaIT.Startup))]
namespace ArquiaIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
