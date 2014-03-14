using WebApp.Domain.Entities;
using WebApp.Web;
using SharpLite.Web.Mvc.ModelBinder;
using StructureMap;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApp
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();

            AutoMapperInitializer.Initialize();
        }
        protected void Application_EndRequest()
        {
            // For SharpLiteEntityFramework
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}
