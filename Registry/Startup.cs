using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Registry.Startup))]
namespace Registry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
