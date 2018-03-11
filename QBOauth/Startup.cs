using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QBOauth.Startup))]
namespace QBOauth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
