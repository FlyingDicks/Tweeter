using Microsoft.Owin;
using FlyingDicksTweeter.Migrations;
using Owin;
using System.Data.Entity;
using FlyingDicksTweeter.Models;

[assembly: OwinStartupAttribute(typeof(FlyingDicksTweeter.Startup))]
namespace FlyingDicksTweeter
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
               new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>()
               );
            ConfigureAuth(app);
        }
    }
}
