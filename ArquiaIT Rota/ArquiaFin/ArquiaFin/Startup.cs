using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArquiaFin.Startup))]
namespace ArquiaFin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
