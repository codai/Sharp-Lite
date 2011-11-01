using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace MyStore.Domain.ProductMgmt
{
    [HasUniqueDomainSignature(ErrorMessage = "A product already exists with the same name")]
    public class Product : Entity
    {
        public Product() {
            Categories = new List<ProductCategory>();
        }

        [DomainSignature]
        [Required(ErrorMessage = "Name must be provided")]
        [StringLength(255, ErrorMessage = "Name must be 255 characters or fewer")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Money is a component, not a separate entity; i.e., the Products table will have column 
        /// for the amount
        /// </summary>
        [DataType("Money")]
        public virtual Money Price { get; set; }

        /// <summary>
        /// many-to-many between Product and ProductCategory
        /// </summary>
        [Display(Name="Product Categories")]
        public virtual IList<ProductCategory> Categories { get; protected set; }
    }
}
