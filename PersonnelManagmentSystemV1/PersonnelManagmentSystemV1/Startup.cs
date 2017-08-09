using Microsoft.Owin;
using Owin;
using Newtonsoft.Json;

[assembly: OwinStartupAttribute(typeof(PersonnelManagmentSystemV1.Startup))]
namespace PersonnelManagmentSystemV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
