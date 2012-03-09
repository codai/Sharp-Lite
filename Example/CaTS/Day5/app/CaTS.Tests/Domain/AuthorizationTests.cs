using NUnit.Framework;
using CaTS.Domain;

namespace CaTS.Tests.Domain
{
    [TestFixture]
    public class AuthorizationTests
    {
        [Test]
        public void CanAuthorizeStaffMemberWithSingleRole() {
            var staffMember = new StaffMember {
                Roles = RoleType.Administrator
            };

            Assert.That(staffMember.IsAuthorizedAs(RoleType.Administrator));
            Assert.That(! staffMember.IsAuthorizedAs(RoleType.SupportStaff));
        }

        [Test]
        public void CanAuthorizeStaffMemberWithMultipleRoles() {
            var staffMember = new StaffMember {
                Roles = RoleType.Manager | RoleType.SupportStaff
            };

            Assert.That(staffMember.IsAuthorizedAs(RoleType.Manager));
            Assert.That(staffMember.IsAuthorizedAs(RoleType.SupportStaff));
            Assert.That(! staffMember.IsAuthorizedAs(RoleType.Administrator));
        }
    }
}
