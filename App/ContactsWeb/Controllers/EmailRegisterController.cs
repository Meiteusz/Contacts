using AutoMapper;
using ContactsWeb.Models;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;

namespace ContactsWeb.Controllers
{
    public class EmailRegisterController : Controller
    {
        private readonly IEmailListController _emailListController;
        private readonly IContactController _contactController;
        private readonly IMapper _mapper;

        public EmailRegisterController(IEmailListController emailListController, IContactController contactController, IMapper _mapper)
        {
            this._emailListController = emailListController;
            this._contactController = contactController;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int contactId)
        {
            ViewBag.ContactName = await _contactController.GetContactNameById(contactId);
            Globals.SetContact(contactId);
            return await Task.Run(() => { return View(); });
        }

        [HttpGet]
        public async Task<IActionResult> RegisterContact()
            => await Task.Run(() => { return RedirectToAction("Index", "Home"); });

        [HttpPost]
        public async Task<IActionResult> RegisterEmailList(EmailListModel emailListModel)
        {
            var emailListCreated = _mapper.Map<EmailList>(emailListModel);
            var response = await _emailListController.RegisterEmailList(emailListCreated);

            if (emailListModel.IsMainEmail)
                await _contactController.SetContactMainEmail(emailListCreated.UserId, emailListModel.Email);

            return View("Index");
        }

        public async Task<IActionResult> ViewContacts()
            => await Task.Run(() => { return RedirectToAction("Index", "ContactsList"); });
    }
}
