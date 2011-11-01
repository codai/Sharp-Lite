using System.Web.Mvc;

namespace TemplateSrc.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
    }
}
