using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CaTS.Domain;
using CaTS.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController(IRepository<StaffMember> staffMemberRepository) {
            _staffMemberRepository = staffMemberRepository;
        }

        public ActionResult Login(string returnUrl) {
            var loginAttempt = new LoginViewModel {
                ReturnUrl = returnUrl
            };

            return View(loginAttempt);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(LoginViewModel loginViewModel) {
            var staffMember = _staffMemberRepository.GetAll().SingleOrDefault(sm => 
                sm.EmployeeNumber == loginViewModel.EmployeeNumber && 
                sm.PasswordHash == loginViewModel.GetPasswordHash());

            if (staffMember != null) {
                FormsAuthentication.SetAuthCookie(loginViewModel.EmployeeNumber, false);
                TempData["message"] = "You have successfully logged in.";
                return Redirect(loginViewModel.ReturnUrl);
            }

            ViewData["message"] = "The login credentials provided were invalid.";
            return View(loginViewModel);
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Session.Abandon();

            var authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "") {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(authenticationCookie);

            var aspNetCookie = new HttpCookie("ASP.NET_SessionId", "") {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(aspNetCookie);

            return RedirectToAction("Index", "Home");
        }

        private readonly IRepository<StaffMember> _staffMemberRepository;
    }
}