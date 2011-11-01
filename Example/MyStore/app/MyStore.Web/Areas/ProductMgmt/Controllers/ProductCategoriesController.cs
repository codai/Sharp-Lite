using System.Linq;
using System.Web.Mvc;
using MyStore.Domain;
using MyStore.Domain.ProductMgmt;
using MyStore.Tasks.ProductMgmt;
using SharpLite.Domain.DataInterfaces;

namespace MyStore.Web.Areas.ProductMgmt.Controllers
{
    public class ProductCategoriesController : Controller
    {
        public ProductCategoriesController(IRepository<ProductCategory> productCategoryRepository, 
            ProductCategoryCudTasks productCategoryMgmtTasks) {

            _productCategoryRepository = productCategoryRepository;
            _productCategoryMgmtTasks = productCategoryMgmtTasks;
        }

        public ActionResult Index() {
            return View(
                _productCategoryRepository.GetAll().OrderBy(pc => pc.Name)
            );
        }

        public ActionResult Details(int id) {
            return View(_productCategoryRepository.Get(id));
        }

        public ActionResult Create() {
            return View("Edit", _productCategoryMgmtTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id) {
            return View(_productCategoryMgmtTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productCategory) {
            if (ModelState.IsValid) {
                ActionConfirmation<ProductCategory> confirmation = _productCategoryMgmtTasks.SaveOrUpdate(productCategory);

                if (confirmation.WasSuccessful) {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index");
                }

                ViewData["message"] = confirmation.Message;
            }

            return View(_productCategoryMgmtTasks.CreateEditViewModel(productCategory));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            ActionConfirmation<ProductCategory> confirmation = _productCategoryMgmtTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly ProductCategoryCudTasks _productCategoryMgmtTasks;
    }
}
