using System.Linq;
using System.Web;
using CaTS.Domain;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Web
{
    public class ContextRegistry : IContextRegistry
    {
        public ContextRegistry(IRepository<StaffMember> staffMemberRepository) {
            _staffMemberRepository = staffMemberRepository;
        }

        public StaffMember GetLoggedInStaffMember() {
            if (! HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            var employeeNumber = HttpContext.Current.User.Identity.Name;
            return _staffMemberRepository.GetAll().Single(x => x.EmployeeNumber == employeeNumber);
        }

        private readonly IRepository<StaffMember> _staffMemberRepository;
    }
}