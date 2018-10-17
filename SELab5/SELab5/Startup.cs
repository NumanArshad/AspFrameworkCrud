using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SELab5.Startup))]
namespace SELab5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
