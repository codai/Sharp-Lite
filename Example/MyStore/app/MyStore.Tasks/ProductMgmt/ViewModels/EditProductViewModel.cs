using System.Collections.Generic;
using MyStore.Domain.ProductMgmt;

namespace MyStore.Tasks.ProductMgmt.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> AvailableProductCategories { get; set; }
    }
}
