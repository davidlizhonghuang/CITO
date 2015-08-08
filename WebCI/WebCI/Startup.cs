using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCI.Startup))]
namespace WebCI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
