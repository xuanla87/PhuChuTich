using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(www.Startup))]
namespace www
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
