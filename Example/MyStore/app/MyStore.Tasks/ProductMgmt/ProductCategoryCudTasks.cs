using System.Linq;
using MyStore.Domain.ProductMgmt;
using SharpLite.Domain.DataInterfaces;
using MyStore.Tasks.ProductMgmt.ViewModels;

namespace MyStore.Tasks.ProductMgmt
{
    /// <summary>
    /// Note that this is called a "CUD" class as it deals only with Create, Update, and Delete.
    /// Querying should be done directly against the repository or via IQueryObject classes.
    /// </summary>
    public class ProductCategoryCudTasks : BaseEntityCudTasks<ProductCategory, EditProductCategoryViewModel>
    {
        public ProductCategoryCudTasks(IRepository<ProductCategory> productCategoryRepository) : base(productCategoryRepository) {
            _productCategoryRepository = productCategoryRepository;
        }

        /// <summary>
        /// Basic view model factory which initializes lists for dropdowns and adds any security 
        /// details to the view model (e.g., .MayAddAttachments) if appropriate.
        /// 
        /// We can't use the CreateEditViewModel() from the base class because we need to add 
        /// categories to the view model which will be shown in a dropdown list.
        /// </summary>
        public override EditProductCategoryViewModel CreateEditViewModel() {
            var viewModel = base.CreateEditViewModel();

            // These will be displayed by the view to optionally select a parent category
            viewModel.AvailableProductCategories = _productCategoryRepository.GetAll().OrderBy(pc => pc.Name);

            return viewModel;
        }

        /// <summary>
        /// We need to replace the base class' version of this because we need to modify the product
        /// category listing before it gets passed to the view.
        /// </summary>
        public override EditProductCategoryViewModel CreateEditViewModel(ProductCategory productCategory) {
            var viewModel = CreateEditViewModel();
            viewModel.ProductCategory = productCategory;

            // Don't allow a category to have itself as a parent
            viewModel.AvailableProductCategories =
                viewModel.AvailableProductCategories.Where(pc => ! pc.Equals(productCategory));

            return viewModel;
        }

        /// <summary>
        /// Udpates an entitry from the DB with new information from the form.  This example 
        /// manually copies the data but you could use a more sophisticated mechanism, like AutoMapper
        /// </summary>
        protected override void TransferFormValuesTo(ProductCategory toUpdate, ProductCategory fromForm) {
            toUpdate.Name = fromForm.Name;
            toUpdate.Parent = fromForm.Parent;
        }

        private readonly IRepository<ProductCategory> _productCategoryRepository;
    }
}
