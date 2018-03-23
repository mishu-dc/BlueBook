using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlueBook.MvcUi.Startup))]
namespace BlueBook.MvcUi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
