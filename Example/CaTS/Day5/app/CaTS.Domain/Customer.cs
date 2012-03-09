using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace CaTS.Domain
{
    [HasUniqueDomainSignature(
        ErrorMessage = "A customer already exists with the provided account number")]
    public class Customer : Entity
    {
        [DomainSignature]
        [Required(ErrorMessage = "Account number must be provided")]
        [StringLength(8, MinimumLength = 8,
            ErrorMessage = "Account number must be exactly 8 characters")]
        [Display(Name = "Account Number")]
        public virtual string AccountNumber { get; set; }

        [Required(ErrorMessage = "First name must be provided")]
        [StringLength(200, 
            ErrorMessage = "First name must be 200 characters or fewer")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Last name must be provided")]
        [StringLength(200,
            ErrorMessage = "Last name must be 200 characters or fewer")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Email]
        [Required(ErrorMessage = "Email address must be provided")]
        [StringLength(200,
            ErrorMessage = "Email must be 200 characters or fewer")]
        [Display(Name = "Email")]
        public virtual string EmailAddress { get; set; }
    }
}