using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyStore.Domain;
using MyStore.Init;
using MyStore.Web.Binders;
using SharpLite.Web.Mvc.ModelBinder;

namespace MyStore.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start() {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            DependencyResolverInitializer.Initialize();

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            ModelBinders.Binders.Add(typeof(Money), new MoneyBinder());
        }
    }
}