using System.Linq;
using System.Web.Mvc;
using CaTS.Domain;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IRepository<StaffMember> staffMemberRepository) {
            _staffMemberRepository = staffMemberRepository;
        }

        public StaffMember GetLoggedInStaffMember() {
            if (! HttpContext.User.Identity.IsAuthenticated)
                return null;

            var employeeNumber = HttpContext.User.Identity.Name;
            return _staffMemberRepository.GetAll().Single(x => x.EmployeeNumber == employeeNumber);
        }

        private readonly IRepository<StaffMember> _staffMemberRepository;
    }
}