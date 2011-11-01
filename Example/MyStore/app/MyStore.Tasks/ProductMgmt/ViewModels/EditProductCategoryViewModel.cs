using System.Collections.Generic;
using MyStore.Domain.ProductMgmt;

namespace MyStore.Tasks.ProductMgmt.ViewModels
{
    public class EditProductCategoryViewModel
    {
        public ProductCategory ProductCategory { get; set; }
        public IEnumerable<ProductCategory> AvailableProductCategories { get; set; }
    }
}
