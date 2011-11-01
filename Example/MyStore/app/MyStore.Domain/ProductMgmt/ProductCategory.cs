using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace MyStore.Domain.ProductMgmt
{
    [HasUniqueDomainSignature(ErrorMessage="A product category already exists with the same name")]
    public class ProductCategory : Entity
    {
        public ProductCategory() {
            Children = new List<ProductCategory>();
            Products = new List<Product>();
        }

        [DomainSignature]
        [Required(ErrorMessage="Name must be provided")]
        [StringLength(255, ErrorMessage="Name must be 255 characters or fewer")]
        public virtual string Name { get; set; }

        /// <summary>
        /// many-to-one from child ProductCategory to parent ProductCategory
        /// </summary>
        [Display(Name="Parent Category")]
        public virtual ProductCategory Parent { get; set; }

        /// <summary>
        /// many-to-many between ProductCategory and Product
        /// </summary>
        public virtual IList<Product> Products { get; protected set; }

        /// <summary>
        /// one-to-many from parent ProductCategory to children ProductCategory
        /// </summary>
        public virtual IList<ProductCategory> Children { get; protected set; }
    }
}
