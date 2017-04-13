using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eating2.Startup))]
namespace Eating2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
