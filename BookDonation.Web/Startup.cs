using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookDonation.Web.Startup))]
namespace BookDonation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
