using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace MyStore.Domain
{
    [HasUniqueDomainSignature(ErrorMessage="A customer already exists with the same first and last name")]
    public class Customer : Entity
    {
        public Customer() {
            Orders = new List<Order>();
        }

        [DomainSignature]
        [Required(ErrorMessage = "First name must be provided")]
        [StringLength(255, ErrorMessage = "First name must be 255 characters or fewer")]
        [Display(Name="First Name")]
        public virtual string FirstName { get; set; }

        [DomainSignature]
        [Required(ErrorMessage = "Last name must be provided")]
        [StringLength(255, ErrorMessage = "Last name must be 255 characters or fewer")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        /// <summary>
        /// Address is a component, not a separate entity; i.e., the Customers table will have 
        /// columns for each property of Address
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// one-to-many from Customer to Order
        /// </summary>
        public virtual IList<Order> Orders { get; protected set; }
    }
}
