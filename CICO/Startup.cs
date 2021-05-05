using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CICO.Startup))]
namespace CICO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
