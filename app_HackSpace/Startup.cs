using Microsoft.Owin;
using Microsoft.WindowsAzure.ServiceRuntime;
using Owin;

[assembly: OwinStartupAttribute(typeof(app_HackSpace.Startup))]
namespace app_HackSpace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
