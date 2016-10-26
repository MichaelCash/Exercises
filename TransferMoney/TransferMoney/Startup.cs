using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransferMoney.Startup))]
namespace TransferMoney
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
