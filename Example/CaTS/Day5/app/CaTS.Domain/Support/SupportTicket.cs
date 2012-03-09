using System;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;

namespace CaTS.Domain.Support
{
    public class SupportTicket : Entity
    {
        public SupportTicket() {
            Status = StatusType.New;
        }

        public virtual string IssueDescription { get; set; }

        public virtual Customer Customer { get; set; }

        [Display(Name = "When Opened")]
        public virtual DateTime WhenOpened { get; set; }

        [Display(Name = "Opened By")]
        public virtual StaffMember OpenedBy { get; set; }

        [Display(Name = "When Resolved")]
        public virtual DateTime? WhenResolved { get; set; }

        [Display(Name = "Issue Type")]
        public virtual IssueType IssueType { get; set; }

        public virtual string Resolution { get; set; }

        public virtual StatusType Status { get; set; }
    }
}
