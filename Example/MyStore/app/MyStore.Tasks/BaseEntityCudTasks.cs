using System;
using System.Reflection;
using System.Text.RegularExpressions;
using MyStore.Domain;
using MyStore.Domain.Validators;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;

namespace MyStore.Tasks
{
    /// <summary>
    /// Base class to consolidate frequently duplicated code from entity-management-task services.
    /// Not required by any means, but helps keep duplicate code to a minimum.
    /// Note that this is called a "CUD" class as it deals only with Create, Update, and Delete.
    /// Querying should be done directly against the repository or via IQueryObject classes.
    /// </summary>
    /// <remarks>If you want to use this with a different ID type (e.g., GUID), 
    /// just change the type constraint below</remarks>
    /// <typeparam name="TEditViewModel">E.g., EditProductCategoryViewModel. The type must have a 
    /// property name identical to T; e.g., ProductCategory</typeparam>
    public abstract class BaseEntityCudTasks<T, TEditViewModel> 
        where T : class, IEntityWithTypedId<int>, new()
        where TEditViewModel : new()
    {
        protected BaseEntityCudTasks(IRepository<T> entityRepository) {
            RequireViewModelToHaveEntityProperty();
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// The view model object must have a settable property of the name typeof(T).Name.
        /// This is so that the CreateEditViewModel() can populate that property 1) with a 
        /// blank entity during creation and 2) with a "data full" entity during edit.
        /// </summary>
        private void RequireViewModelToHaveEntityProperty() {
            PropertyInfo entityProperty = typeof (TEditViewModel).GetProperty(typeof (T).Name);

            if (entityProperty == null)
                throw new MissingMemberException(typeof (TEditViewModel).Name + 
                    " was expected to have a property named " + typeof (T).Name + ". This property " + 
                    "is needed for the support of the base CreateEditViewModel methods.  When editing an existing");

            MethodInfo entitySetter = entityProperty.GetSetMethod();

            if (entitySetter == null)
                throw new MissingMemberException(typeof (TEditViewModel).Name + 
                    " was expected to have a setter for property named " + typeof (T).Name + ".");
        }

        public virtual TEditViewModel CreateEditViewModel() {
            var viewModel = new TEditViewModel();

            // Set the entity property to a "blank" entity
            viewModel.GetType().GetProperty(typeof(T).Name).SetValue(viewModel, new T(), null);
            
            return viewModel;
        }

        public virtual TEditViewModel CreateEditViewModel(T entity) {
            // This calls the basic creation method so that any other information, such as dropdown 
            // list data or otherwise may be bound to the view model
            var viewModel = CreateEditViewModel();

            // Set the entity property to the "data filled" entity provided
            viewModel.GetType().GetProperty(typeof(T).Name).SetValue(viewModel, entity, null);
            
            return viewModel;
        }

        public virtual TEditViewModel CreateEditViewModel(int id) {
            return CreateEditViewModel(_entityRepository.Get(id));
        }

        /// <summary>
        /// Udpates an entitry from the DB with new information from the form.
        /// </summary>
        protected abstract void TransferFormValuesTo(T toUpdate, T fromForm);

        /// <param name="fromForm">Must be a not-null, valid object</param>
        public virtual ActionConfirmation<T> SaveOrUpdate(T fromForm) {
            if (fromForm == null) throw new ArgumentNullException("fromForm may not be null");
            if (!DataAnnotationsValidator.TryValidate(fromForm)) throw new InvalidOperationException("fromForm is in an invalid state");

            // If the ID of fromForm is 0 then we know we're saving a brand new one
            if (fromForm.IsTransient()) {
                _entityRepository.SaveOrUpdate(fromForm);

                return ActionConfirmation<T>.CreateSuccessConfirmation(
                    "The " + GetFriendlyNameOfType() + " was successfully saved.", fromForm);
            }

            // But if the ID of fromForm is > 0, then we know we're updating an existing one.
            // Always update the existing one instead of simply "updating" the item from the form.  Otherwise,
            // you risk the loss of important data, such as references not captured by the form or audit 
            // details that exists from when the object was initially saved.
            T toUpdate = _entityRepository.Get(fromForm.Id);
            TransferFormValuesTo(toUpdate, fromForm);

            // Since any changes will be automatically persisted to the DB when the current transaction
            // is committed, we want to make sure it's in a valid state after being updated.  If it's 
            // not, it's most likely a development bug. In design-by-contract speak, this is an "ensure check."
            if (!DataAnnotationsValidator.TryValidate(toUpdate))
                throw new InvalidOperationException("The " + GetFriendlyNameOfType() + " could not be updated due to missing or invalid information");

            return ActionConfirmation<T>.CreateSuccessConfirmation(
                "The " + GetFriendlyNameOfType() + " was successfully updated.", fromForm);
        }

        public virtual ActionConfirmation<T> Delete(int id) {
            T toDelete = _entityRepository.Get(id);

            if (toDelete != null) {
                try {
                    _entityRepository.Delete(toDelete);
                    return ActionConfirmation<T>.CreateSuccessConfirmation(
                        "The " + GetFriendlyNameOfType() + " was successfully deleted.", toDelete);
                }
                // A foreign key constraint violation will thrown an exception.  As inadvisable as it use
                // to use exceptions as a means of business logic, this is certainly the easiest way to to do it.
                catch {
                    // Since we're swallowing the exception, we want to make sure the transaction gets rolled back
                    _entityRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation<T>.CreateFailureConfirmation(
                        "The " + GetFriendlyNameOfType() + " could not be deleted; another item depends on it.", toDelete);
                }
            }

            return ActionConfirmation<T>.CreateFailureConfirmation(
                "The " + GetFriendlyNameOfType() + " could not be found for deletion. It may already have been deleted.", default(T));
        }

        protected string GetFriendlyNameOfType() {
            if (string.IsNullOrEmpty(_friendlyNameOfType)) {
                Regex r = new Regex("([A-Z]+[a-z]+)");
                _friendlyNameOfType = r.Replace(typeof(T).Name, m => m.Value.ToLower() + " ");
            }

            return _friendlyNameOfType;
        }

        private string _friendlyNameOfType;
        private readonly IRepository<T> _entityRepository;
    }
}
