using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTBOOK_1670_.Startup))]
namespace FPTBOOK_1670_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
