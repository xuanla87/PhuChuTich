using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChuyenData.Startup))]
namespace ChuyenData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
