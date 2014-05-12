using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCloudClassroom.Startup))]
namespace MyCloudClassroom
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
