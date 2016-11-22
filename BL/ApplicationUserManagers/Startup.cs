using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BEP.BL.Startup))]
namespace BEP.BL
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
    }
  }
}
