using System;
using System.Linq;
using System.Web.Mvc;
using SharpLite.Domain;

namespace SharpLite.Web.Mvc.ModelBinder
{
    internal class EntityValueBinder : SharpModelBinder
    {
        /// <summary>
        ///     Binds the model value to an entity by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        ///     The bound value.
        /// </returns>
        /// <param name = "controllerContext">The controller context.</param>
        /// <param name = "bindingContext">The binding context.</param>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            Type modelType = bindingContext.ModelType;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != null) {
                Type entityInterfaceType =
                    modelType.GetInterfaces().First(
                        interfaceType =>
                        interfaceType.IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));

                Type idType = entityInterfaceType.GetGenericArguments().First();
                string rawId = (valueProviderResult.RawValue as string[]).First();

                if (string.IsNullOrEmpty(rawId)) {
                    return null;
                }

                try {
                    object typedId = (idType == typeof(Guid)) ? new Guid(rawId) : Convert.ChangeType(rawId, idType);
                    return EntityRetriever.GetEntityFor(modelType, typedId, idType);
                }
                catch (Exception) {
                    // If the Id conversion failed for any reason, just return null
                    return null;
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}