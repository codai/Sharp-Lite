using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Domain.Validators
{
    public class DataAnnotationsValidator
    {
        public static bool TryValidate(object @object) {
            ICollection<ValidationResult> ignoredResults;
            return TryValidate(@object, out ignoredResults);
        }

        public static bool TryValidate(object @object, out ICollection<ValidationResult> results) {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(
                @object, context, results,
                validateAllProperties: true
            );
        }
    }
}
