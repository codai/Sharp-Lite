using MyStore.Domain;
using MyStore.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace MyStore.Tasks
{
    public class CustomerCudTasks : BaseEntityCudTasks<Customer, EditCustomerViewModel>
    {
        public CustomerCudTasks(IRepository<Customer> customerRepository) 
            : base(customerRepository) { }

        protected override void TransferFormValuesTo(Customer toUpdate, Customer fromForm) {
            toUpdate.FirstName = fromForm.FirstName;
            toUpdate.LastName = fromForm.LastName;
            toUpdate.Address.StreetAddress = fromForm.Address.StreetAddress;
            toUpdate.Address.ZipCode = fromForm.Address.ZipCode;
        }
    }
}
