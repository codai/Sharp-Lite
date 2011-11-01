using System.Linq;
using System.Web.Mvc;
using MyStore.Domain;
using MyStore.Tasks;
using SharpLite.Domain.DataInterfaces;
using MyStore.Domain.Queries;

namespace MyStore.Web.Areas.CustomerMgmt.Controllers
{
    public class CustomersController : Controller
    {
        public CustomersController(IRepository<Customer> customerRepository, 
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

        public ActionResult ShowCustomerOrderSummaries() {
            IQueryable<CustomerOrderSummaryDto> summaries = 
                _customerRepository.GetAll()
                    .FindActiveCustomers(2)
                    .QueryForCustomerOrderSummaries();

            return View(summaries);
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
