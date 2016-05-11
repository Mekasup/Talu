using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Talu.Startup))]
namespace Talu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
