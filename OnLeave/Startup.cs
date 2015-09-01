using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnLeave.Startup))]
namespace OnLeave
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        
    }
}
