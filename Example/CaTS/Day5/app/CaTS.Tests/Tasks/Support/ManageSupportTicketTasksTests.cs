using CaTS.Tasks.Support;
using NUnit.Framework;

namespace CaTS.Tests.Tasks.Support
{
    [TestFixture]
    public class ManageSupportTicketTasksTests
    {
        [Test]
        public void CanCreateOpenSupportTicketViewModel() {
            var tasks = new OpenSupportTicketTasks(null, null, null, null);

            var viewModel = tasks.CreateOpenSupportTicketViewModel();
        }
    }
}
