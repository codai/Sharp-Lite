using System.Web.Mvc;

namespace MyStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
    }
}
