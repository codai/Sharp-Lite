using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace CaTS.Domain
{
    [HasUniqueDomainSignature(
        ErrorMessage="A staff member already exists with the same employee number")]
    public class StaffMember : Entity
    {
        [Required(ErrorMessage = "Employee number must be provided")]
        [StringLength(6, MinimumLength = 6, 
            ErrorMessage = "Employee number must be 6 characters long")]
        [Display(Name = "Employee Number")]
        public virtual string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "First name must be provided")]
        [StringLength(50, ErrorMessage = "First name must be 50 characters or fewer")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Last name must be provided")]
        [StringLength(50, ErrorMessage = "Last name must be 50 characters or fewer")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Required(ErrorMessage = "PasswordHash must be provided")]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Represents one or more roles that the staff member has been assigned.
        /// E.g., set property to RoleType.Manager for one role and 
        /// (RoleType.Manager | RoleType.SupportStaff) for two roles.
        /// </summary>
        public virtual RoleType Roles { get; set; }

        public virtual bool IsAuthorizedAs(RoleType requiredRoles) {
            // Check if the roles enum has the specific role bit set
            return (requiredRoles & Roles) == requiredRoles;
        }
    }
}
