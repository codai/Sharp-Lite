using System.Web.Mvc;
using CaTS.Domain;
using CaTS.Web.Controllers.Attributes;

namespace CaTS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }

        [RequireRole(RoleType.Administrator)]
        public ActionResult AdminPage() {
            return View();
        }
    }
}
