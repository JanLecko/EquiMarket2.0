using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EquiMarket.Startup))]
namespace EquiMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
