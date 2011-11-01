using System.Web.Mvc;
using MyStore.Domain;

namespace MyStore.Web.Binders
{
    /// <summary>
    /// Custom binder for binding input to Money object
    /// </summary>
    public class MoneyBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Amount");

            string attemptedValue = value.AttemptedValue;

            decimal amount;
            bool couldAmountBeParsed = decimal.TryParse(attemptedValue, out amount);

            return couldAmountBeParsed
                ? new Money(amount)
                : null;
        }
    }
}
