using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppMvc.Startup))]
namespace AppMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
