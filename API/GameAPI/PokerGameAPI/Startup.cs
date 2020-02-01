

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PokerGameAPI.Startup))]

namespace PokerGameAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
