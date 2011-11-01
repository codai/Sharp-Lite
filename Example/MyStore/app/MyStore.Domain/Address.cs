using System.ComponentModel.DataAnnotations;

namespace MyStore.Domain
{
    /// <summary>
    /// This is a component of Customer
    /// </summary>
    public class Address
    {
        [StringLength(255, ErrorMessage = "Street address must be 255 characters or fewer")]
        [Display(Name = "Street Address")]
        public virtual string StreetAddress { get; set; }

        [Required(ErrorMessage = "Zip code must be provided")]
        [StringLength(5, ErrorMessage = "Zip code must be 5 characters or fewer")]
        [Display(Name = "Zip Code")]
        public virtual string ZipCode { get; set; }
    }
}
