using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRiVote.Startup))]
namespace HRiVote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
