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
        private readonly IUserController _userController;
        private readonly IMapper _mapper;

        public EmailRegisterController(IEmailListController emailListController, IUserController userController, IMapper _mapper)
        {
            this._emailListController = emailListController;
            this._userController = userController;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int idContact)
        {
            ViewBag.ContactName = await _userController.GetContactNameById(idContact);
            Globals.SetContact(idContact);
            return await Task.Run(() => { return View(); });
        }

        public async Task<IActionResult> RegisterContact()
            => await Task.Run(() => { return RedirectToAction("Index", "Home"); });

        [HttpPost]
        public async Task<IActionResult> RegisterEmailList(EmailListModel emailListModel)
        {
            var emailListCreated = _mapper.Map<EmailList>(emailListModel);
            var response = await _emailListController.RegisterEmailList(emailListCreated, emailListModel.IsMainEmail);
            return View("Index");
        }

        public async Task<IActionResult> ViewContacts()
            => await Task.Run(() => { return RedirectToAction("Index", "ContactsList"); });
    }
}
