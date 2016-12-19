using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EuroSpaceCenter.Startup))]
namespace EuroSpaceCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
