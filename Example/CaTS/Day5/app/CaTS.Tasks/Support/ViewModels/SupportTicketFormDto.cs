using System.ComponentModel.DataAnnotations;
using CaTS.Domain;
using CaTS.Domain.Support;
using CaTS.Domain.Validators;

namespace CaTS.Tasks.Support.ViewModels
{
    [NewOrSelectedIssueTypeIsRequired(ErrorMessage = "A new or existing issue type must be specified")]
    [NewOrSelectedCustomerIsRequired(ErrorMessage = "A new or existing customer must be specified")]
    public class SupportTicketFormDto
    {
        public SupportTicketFormDto() {
            Status = StatusType.New;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Description of issue must be provided")]
        [Display(Name = "Description of Issue")]
        public virtual string IssueDescription { get; set; }

        [Required(ErrorMessage = "Customer must be provided")]
        public virtual Customer Customer { get; set; }

        public Customer NewCustomer { get; set; }
        
        public IssueType NewIssueType { get; set; }

        [Display(Name = "Issue Type")]
        public virtual IssueType IssueType { get; set; }

        [RequiredIf("SupportTicket_Status", (int) StatusType.Resolved, 
            ErrorMessage = "Resolution is required if ticket is marked as resolved")]
        public virtual string Resolution { get; set; }

        [Required(ErrorMessage = "Status must be selected")]
        public virtual StatusType Status { get; set; }

        private class NewOrSelectedIssueTypeIsRequiredAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) {
                var supportTicketViewModel = (SupportTicketFormDto)value;

                return supportTicketViewModel != null &&
                       ((supportTicketViewModel.IssueType != null &&
                       !supportTicketViewModel.IssueType.IsTransient()) || 
                       supportTicketViewModel.NewIssueType != null);
            }
        }

        private class NewOrSelectedCustomerIsRequiredAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) {
                var supportTicketViewModel = (SupportTicketFormDto)value;

                return supportTicketViewModel != null &&
                       ((supportTicketViewModel.Customer != null &&
                       !supportTicketViewModel.Customer.IsTransient()) ||
                       supportTicketViewModel.NewCustomer != null);
            }
        }
    }
}
