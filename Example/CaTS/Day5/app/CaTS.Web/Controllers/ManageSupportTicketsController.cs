using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CaTS.Domain;
using CaTS.Domain.Support;
using CaTS.Domain.Validators;
using CaTS.Tasks.Support;
using CaTS.Tasks.Support.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace CaTS.Web.Controllers
{
    public class ManageSupportTicketsController : Controller
    {
        public ManageSupportTicketsController(IRepository<SupportTicket> supportTicketRepository, 
            OpenSupportTicketTasks openSupportTicketTasks) {
            if (openSupportTicketTasks == null) throw new ArgumentNullException("openSupportTicketTasks is null");

            _openSupportTicketTasks = openSupportTicketTasks;
            _supportTicketRepository = supportTicketRepository;
        }

        public ActionResult Index() {
            return View(_supportTicketRepository.GetAll()
                .OrderByDescending(x => x.WhenOpened));
        }

        public ActionResult Details(int id) {
            return View(_supportTicketRepository.Get(id));
        }

        public ActionResult Open() {
            return View("Edit", _openSupportTicketTasks.CreateOpenSupportTicketViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupportTicketFormDto supportTicketFormDto) {
            if (ModelState.IsValid) {
                ActionConfirmation<SupportTicket> confirmation =
                    _openSupportTicketTasks.Open(supportTicketFormDto);

                if (confirmation.WasSuccessful) {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index");
                }

                ViewData["message"] = confirmation.Message;
            }

            return View(_openSupportTicketTasks.CreateOpenSupportTicketViewModel(supportTicketFormDto));
        }

        private readonly OpenSupportTicketTasks _openSupportTicketTasks;
        private readonly IRepository<SupportTicket> _supportTicketRepository;
    }
}