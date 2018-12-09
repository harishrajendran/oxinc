using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeaveManagement.Startup))]
namespace LeaveManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
