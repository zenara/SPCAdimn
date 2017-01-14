using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminSiteMVC.Startup))]
namespace AdminSiteMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
