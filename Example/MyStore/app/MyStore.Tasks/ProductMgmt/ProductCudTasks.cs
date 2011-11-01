using System.Linq;
using MyStore.Domain.ProductMgmt;
using SharpLite.Domain.DataInterfaces;
using MyStore.Tasks.ProductMgmt.ViewModels;

namespace MyStore.Tasks.ProductMgmt
{
    public class ProductCudTasks : BaseEntityCudTasks<Product, EditProductViewModel>
    {
        public ProductCudTasks(IRepository<Product> productRepository, IRepository<ProductCategory> productCategoryRepository) 
            : base(productRepository) {

            _productCategoryRepository = productCategoryRepository;
        }

        /// <summary>
        /// We only need to replace the basic CreateEditViewModel method in order to add categories
        /// to the view model which will be used to populate a drop down list.
        /// </summary>
        public override EditProductViewModel CreateEditViewModel() {
            var viewModel = base.CreateEditViewModel();

            // These will be displayed by the view to select one or more product-category relationships
            viewModel.AvailableProductCategories = _productCategoryRepository.GetAll().OrderBy(pc => pc.Name);

            return viewModel;
        }

        protected override void TransferFormValuesTo(Product toUpdate, Product fromForm) {
            toUpdate.Name = fromForm.Name;
            toUpdate.Price = fromForm.Price;

            toUpdate.Categories.Clear();

            foreach (var category in fromForm.Categories) {
                toUpdate.Categories.Add(category);
            }
        }

        private readonly IRepository<ProductCategory> _productCategoryRepository;
    }
}
