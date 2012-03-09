using System.Linq;
using System.Web.Mvc;
using CaTS.Domain;
using CaTS.Tasks;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Web.Controllers
{
    public class ManageCustomersController : Controller
    {
        public ManageCustomersController(IRepository<Customer> customerRepository,
            CustomerCudTasks customerMgmtTasks) {

            _customerRepository = customerRepository;
            _customerMgmtTasks = customerMgmtTasks;
        }

        public ActionResult Index() {
            return View(
                _customerRepository.GetAll()
                    .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
            );
        }

        public ActionResult Details(int id) {
            return View(_customerRepository.Get(id));
        }

        public ActionResult Create() {
            return View("Edit", _customerMgmtTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id) {
            return View(_customerMgmtTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer) {
            if (ModelState.IsValid) {
                ActionConfirmation<Customer> confirmation = _customerMgmtTasks.SaveOrUpdate(customer);

                if (confirmation.WasSuccessful) {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index");
                }

                ViewData["message"] = confirmation.Message;
            }

            return View(_customerMgmtTasks.CreateEditViewModel(customer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            ActionConfirmation<Customer> confirmation = _customerMgmtTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IRepository<Customer> _customerRepository;
        private readonly CustomerCudTasks _customerMgmtTasks;
    }
}
