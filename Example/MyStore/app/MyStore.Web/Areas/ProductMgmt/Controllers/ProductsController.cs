using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyStore.Domain;
using MyStore.Domain.ProductMgmt;
using MyStore.Domain.ProductMgmt.Queries;
using MyStore.Tasks.ProductMgmt;
using SharpLite.Domain.DataInterfaces;

namespace MyStore.Web.Areas.ProductMgmt.Controllers
{
    public class ProductsController : Controller
    {
        /// <param name="queryForProductOrderSummaries">This is injected via the IoC; usually, we 
        /// would simply invoke a query object directly.  But this query requires leveraging the
        /// underlying data-access mechanism directly (e.g., NHibernate) and therefore has an 
        /// interface to avoid having a direct dependency on NHibernate from this project</param>
        public ProductsController(IRepository<Product> productRepository,
            ProductCudTasks productMgmtTasks, IQueryForProductOrderSummaries queryForProductOrderSummaries) {

            _productRepository = productRepository;
            _productMgmtTasks = productMgmtTasks;
            _queryForProductOrderSummaries = queryForProductOrderSummaries;
        }

        public ActionResult Index() {
            return View(
                _productRepository.GetAll().OrderBy(p => p.Name)
            );
        }

        public ActionResult Details(int id) {
            return View(_productRepository.Get(id));
        }

        public ActionResult Create() {
            return View("Edit", _productMgmtTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id) {
            return View(_productMgmtTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product) {
            if (ModelState.IsValid) {
                ActionConfirmation<Product> confirmation = _productMgmtTasks.SaveOrUpdate(product);

                if (confirmation.WasSuccessful) {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index");
                }

                ViewData["message"] = confirmation.Message;
            }

            return View(_productMgmtTasks.CreateEditViewModel(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            ActionConfirmation<Product> confirmation = _productMgmtTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index");
        }

        public ActionResult ShowProductOrderSummaries() {
            IEnumerable<ProductOrderSummaryDto> summaries =
                _queryForProductOrderSummaries.GetByDateRange(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);

            return View(summaries);
        }

        private readonly IRepository<Product> _productRepository;
        private readonly ProductCudTasks _productMgmtTasks;
        private readonly IQueryForProductOrderSummaries _queryForProductOrderSummaries;
    }
}
