using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.App_Start;
using WebApp.Models;

namespace WebApp {

  public class MvcApplication : System.Web.HttpApplication {

    protected void Application_Start() {

      Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebAppDbContext, Migrations.Configuration>());

      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
  }
}