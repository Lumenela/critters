using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Critters.Startup))]
namespace Critters
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
