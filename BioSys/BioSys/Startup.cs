using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BioSys.Startup))]
namespace BioSys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
