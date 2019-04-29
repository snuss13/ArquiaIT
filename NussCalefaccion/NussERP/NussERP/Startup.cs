using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NussERP.Startup))]
namespace NussERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
