using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugMania.Startup))]
namespace BugMania
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
