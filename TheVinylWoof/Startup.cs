using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheVinylWoof.Startup))]
namespace TheVinylWoof
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
