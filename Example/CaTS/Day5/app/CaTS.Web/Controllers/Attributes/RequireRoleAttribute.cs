using System;
using System.Web;
using System.Web.Mvc;
using CaTS.Domain;
using System.Linq;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Web.Controllers.Attributes
{
    /// <summary>
    /// See http://stackoverflow.com/questions/4837103/asp-net-mvc-alternative-to-role-provider
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Struct | 
        AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
    public class RequireRoleAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public RequireRoleAttribute(RoleType requiredRoles) {
            _requiredRoles = requiredRoles;
            _staffMemberRepository = DependencyResolver.Current.GetService<IRepository<StaffMember>>(); ;
        }

        public void OnAuthorization(AuthorizationContext filterContext) {
            var loggedInStaffMember = GetLoggedInStaffMember();
            var success = loggedInStaffMember.IsAuthorizedAs(_requiredRoles);

            if (success) {
                var cache = filterContext.HttpContext.Response.Cache;
                cache.SetProxyMaxAge(new TimeSpan(0));
                cache.AddValidationCallback(
                    delegate(HttpContext context, object data, ref HttpValidationStatus validationStatus) {
                        validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
                    }, null);
            }
            else {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            filterContext.Result = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()
                                       ? new HttpStatusCodeResult(500)
                                       : new HttpUnauthorizedResult();
        }

        public HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext) {
            var loggedInStaffMember = GetLoggedInStaffMember();
            var isAuthorized = loggedInStaffMember.IsAuthorizedAs(_requiredRoles);

            return isAuthorized ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }

        private StaffMember GetLoggedInStaffMember() {
            var employeeNumber = HttpContext.Current.User.Identity.Name;
            return _staffMemberRepository.GetAll().Single(x => x.EmployeeNumber == employeeNumber);
        }

        private readonly RoleType _requiredRoles;
        private readonly IRepository<StaffMember> _staffMemberRepository;
    }
}