using System;
using System.Linq;
using CaTS.Domain;
using CaTS.Domain.Support;
using CaTS.Domain.Utilities;
using CaTS.Domain.Validators;
using CaTS.Tasks.Support.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Tasks.Support
{
    public class OpenSupportTicketTasks
    {
        public OpenSupportTicketTasks(IRepository<SupportTicket> supportTicketRepository, IContextRegistry contextRegistry,
            IRepository<Customer> customerRepository, IRepository<IssueType> issueTypeRepository) {

            if (supportTicketRepository == null) throw new ArgumentNullException("supportTicketRepository is null");
            if (issueTypeRepository == null) throw new ArgumentNullException("issueTypeRepository is null");
            if (customerRepository == null) throw new ArgumentNullException("customerRepository is null");
            if (contextRegistry == null) throw new ArgumentNullException("contextRegistry is null");

            _issueTypeRepository = issueTypeRepository;
            _supportTicketRepository = supportTicketRepository;
            _customerRepository = customerRepository;
            _contextRegistry = contextRegistry;
        }

        public OpenSupportTicketViewModel CreateOpenSupportTicketViewModel() {
            return CreateOpenSupportTicketViewModel(new SupportTicketFormDto());
        }

        public OpenSupportTicketViewModel CreateOpenSupportTicketViewModel(SupportTicketFormDto supportTicketFormDto) {
            var statusTypes =
                from StatusType statusType in Enum.GetValues(typeof(StatusType))
                select new Pair<int, string>((int) statusType, EnumUtils.GetEnumDescription(statusType));
            
            var viewModel = new OpenSupportTicketViewModel {
                SupportTicketFormDto = supportTicketFormDto,
                IssueTypes = _issueTypeRepository.GetAll()
                    .OrderBy(x => x.Name),
                LoggedInStaffMember = _contextRegistry.GetLoggedInStaffMember(),
                StatusTypes = statusTypes
            };

            return viewModel;
        }

        public ActionConfirmation<SupportTicket> Open(SupportTicketFormDto supportTicketFormDto) {
            if (supportTicketFormDto == null) throw new ArgumentNullException("supportTicketFormDto is null");
            if (!DataAnnotationsValidator.TryValidate(supportTicketFormDto))
                throw new InvalidOperationException("supportTicketFormDto is in an invalid state");

            var supportTicketToSave = supportTicketFormDto.Id > 0
                ? _supportTicketRepository.Get(supportTicketFormDto.Id)
                : CreateNewSupportTicket(supportTicketFormDto);

            TransferFormValuesTo(supportTicketToSave, supportTicketFormDto);

            var customerConfirmationMessage = HandleNewCustomer(supportTicketFormDto.NewCustomer, supportTicketToSave);
            var issueConfirmationMessage = HandleNewIssueType(supportTicketFormDto.NewIssueType, supportTicketToSave);
            _supportTicketRepository.SaveOrUpdate(supportTicketToSave);

            return ActionConfirmation<SupportTicket>
                .CreateSuccessConfirmation("Support ticket #" + supportTicketToSave.Id + " has been opened." +
                    customerConfirmationMessage + issueConfirmationMessage, supportTicketToSave);
        }

        private void TransferFormValuesTo(SupportTicket supportTicketToSave, SupportTicketFormDto supportTicketFormDto) {
            // Note the WhenResolved time if going from not-resolved to resolved
            if (supportTicketToSave.Status != StatusType.Resolved && supportTicketFormDto.Status == StatusType.Resolved)
                supportTicketToSave.WhenResolved = DateTime.UtcNow;

            supportTicketToSave.IssueDescription = supportTicketFormDto.IssueDescription;
            supportTicketToSave.Resolution = supportTicketFormDto.Resolution;
            supportTicketToSave.Status = supportTicketFormDto.Status;
            supportTicketToSave.Customer = supportTicketFormDto.Customer;
            supportTicketToSave.IssueType = supportTicketFormDto.IssueType;
        }

        private SupportTicket CreateNewSupportTicket(SupportTicketFormDto supportTicketFormDto) {
            var supportTicketToSave = new SupportTicket {
                OpenedBy = _contextRegistry.GetLoggedInStaffMember(),
                WhenOpened = DateTime.UtcNow
            };

            supportTicketToSave.WhenResolved = supportTicketFormDto.Status == StatusType.Resolved
                ? supportTicketToSave.WhenOpened
                : new DateTime?();

            return supportTicketToSave;
        }

        private string HandleNewIssueType(IssueType newIssueType, SupportTicket supportTicket) {
            if (newIssueType != null && newIssueType.IsTransient()) {
                // Validation should already have been enforced
                if (!DataAnnotationsValidator.TryValidate(newIssueType))
                    throw new InvalidOperationException("newIssueType is in an invalid state");

                _issueTypeRepository.SaveOrUpdate(newIssueType);
                supportTicket.IssueType = newIssueType;

                return " The issue type '" + newIssueType.Name + "' has been added.";
            }

            return null;
        }

        private string HandleNewCustomer(Customer newCustomer, SupportTicket supportTicket) {
            if (newCustomer != null && newCustomer.IsTransient()) {
                // Validation should already have been enforced
                if (!DataAnnotationsValidator.TryValidate(newCustomer))
                    throw new InvalidOperationException("newCustomer is in an invalid state");

                _customerRepository.SaveOrUpdate(newCustomer);
                supportTicket.Customer = newCustomer;

                return " The newCustomer with account number " + 
                    newCustomer.AccountNumber + " has been added.";
            }

            return null;
        }

        private readonly IRepository<IssueType> _issueTypeRepository;
        private readonly IRepository<SupportTicket> _supportTicketRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IContextRegistry _contextRegistry;
    }
}
