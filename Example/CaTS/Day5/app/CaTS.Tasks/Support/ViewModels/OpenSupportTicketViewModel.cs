using System.Collections.Generic;
using CaTS.Domain;
using CaTS.Domain.Support;
using CaTS.Domain.Utilities;

namespace CaTS.Tasks.Support.ViewModels
{
    public class OpenSupportTicketViewModel
    {
        public OpenSupportTicketViewModel() {
            SupportTicketFormDto = new SupportTicketFormDto();
        }

        public SupportTicketFormDto SupportTicketFormDto { get; set; }
        public StaffMember LoggedInStaffMember { get; set; }
        public IEnumerable<IssueType> IssueTypes;
        public IEnumerable<Pair<int, string>> StatusTypes;
    }
}
