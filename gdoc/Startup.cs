using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gdoc.Startup))]
namespace gdoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
