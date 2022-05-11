using AutoMapper;
using ContactsWeb.Models;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace ContactsWeb.Controllers
{
    public class ContactsListController : Controller
    {
        private readonly IContactController _contactsController;
        private readonly IEmailListController _emailListController;
        private readonly IMapper _mapper;

        public ContactsListController(IContactController contactController, IEmailListController emailListController, IMapper mapper)
        {
            this._contactsController = contactController;
            this._emailListController = emailListController;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactListModel = MappingProfile.UserModelReverseMap(_contactsController.GetAllContacts().Result.Results.ToList());
            return await Task.Run(() => { return View(contactListModel); });
        }

        public async Task<IActionResult> Search(ContactModel contactModel)
        {
            var contactFiltered = _mapper.Map<Contact>(contactModel);
            var contactsListFiltered = _contactsController.GetContactsByFilters(contactFiltered).Result.Results;
            var contactsListFilteredModel = MappingProfile.UserModelReverseMap(contactsListFiltered.ToList());

            return await Task.Run(() => { return View("Index", contactsListFilteredModel); });
        }

        public async Task<IActionResult> ClearRefresh()
        {
            var contactListModel = MappingProfile.UserModelReverseMap(_contactsController.GetAllContacts().Result.Results.ToList());
            return await Task.Run(() => { return View("Index", contactListModel); });
        }

        
        public async Task<IActionResult> EditContact(int contactId)
        {
            var contactsEmailListModel = MappingProfile.UniqueUserModelReverseMap(await _contactsController.GetFirstOrDefault(contactId));
            var emailList = _emailListController.GetAllContactEmails(contactId).Result.Results;
            contactsEmailListModel.ContactsEmailList = MappingProfile.EmailListModelReverseMap(emailList.ToList());

            return await Task.Run(() => { return View(contactsEmailListModel); });
        }

        public async Task<IActionResult> DeleteContact(int contactId)
        {
            var response = await _contactsController.DeleteContact(contactId);

            if (response.Success)
            {
                return RedirectToAction("Index", "ContactsList");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ViewContactsList(int contactId)
            => await Task.Run(() => RedirectToAction("Index", "ContactsList", new { contactId = contactId }));

        public async Task<IActionResult> SaveContact(ContactsEmailListModel contactsEmailListModel)
        {
            var contact = _mapper.Map<Contact>(contactsEmailListModel.ContactModel);
            var response = await _contactsController.UpdateContact(contact);
            return RedirectToAction("EditContact", "ContactsList", new { contactId = response.IdReturn } );
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEditContactEmails(int contactId, int idEmail, bool isMainEmail, string email)
        {
            var response = await _emailListController.UpdateEmail(idEmail, email);

            if (isMainEmail)
                await _contactsController.SetContactMainEmail(contactId, email);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CheckMainEmail(int contactId, string email)
        {
            var isMainEmail = await _contactsController.CheckMainEmail(contactId, email);
            return Json(isMainEmail);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailIdByEmail(string email)
        {
            var emailId = await _emailListController.GetEmailIdByEmail(email);
            return Json(emailId);
        }
    }
}
