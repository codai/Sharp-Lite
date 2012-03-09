using CaTS.Domain;
using CaTS.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Tasks
{
    public class CustomerCudTasks : BaseEntityCudTasks<Customer, EditCustomerViewModel>
    {
        public CustomerCudTasks(IRepository<Customer> customerRepository) 
            : base(customerRepository) {
        }

        protected override void TransferFormValuesTo(Customer toUpdate, Customer fromForm) {
            toUpdate.AccountNumber = (fromForm.AccountNumber ?? "").Trim();
            toUpdate.EmailAddress = (fromForm.EmailAddress ?? "").Trim();
            toUpdate.AccountNumber = (fromForm.AccountNumber ?? "").Trim();
            toUpdate.AccountNumber = (fromForm.AccountNumber ?? "").Trim();
        }
    }
}
