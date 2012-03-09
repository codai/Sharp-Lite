using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace CaTS.Domain.Support
{
    [HasUniqueDomainSignature(ErrorMessage = "An issue type already exists with the same description")]
    public class IssueType : Entity
    {
        [DomainSignature]
        [Required(ErrorMessage = "Name of the issue type must be provided")]
        [StringLength(200,
            ErrorMessage = "Name of the issue type must be 200 characters or fewer")]
        public virtual string Name { get; set; }
    }
}
      