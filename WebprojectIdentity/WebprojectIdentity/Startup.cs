using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebprojectIdentity.Startup))]
namespace WebprojectIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
