using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NCBPlacementWebsite.Startup))]
namespace NCBPlacementWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
