using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TCC.Startup))]
namespace TCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
