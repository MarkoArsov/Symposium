using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Symposium.Startup))]
namespace Symposium
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
