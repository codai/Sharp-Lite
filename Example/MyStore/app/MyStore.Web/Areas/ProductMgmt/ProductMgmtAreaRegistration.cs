using System.Web.Mvc;

namespace MyStore.Web.Areas.ProductMgmt
{
    public class ProductMgmtAreaRegistration : AreaRegistration
    {
        public override string AreaName {
            get {
                return "ProductMgmt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "ProductMgmt_default",
                "ProductMgmt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
