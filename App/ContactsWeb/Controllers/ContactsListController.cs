using AutoMapper;
using ContactsWeb.Models;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace ContactsWeb.Controllers
{
    public class ContactsListController : Controller
    {
        private readonly IUserController _contactsController;
        private readonly IEmailListController _emailListController;
        private readonly IMapper _mapper;

        public ContactsListController(IUserController userController, IEmailListController emailListController, IMapper mapper)
        {
            this._contactsController = userController;
            this._emailListController = emailListController;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var contactListModel = MappingProfile.UserModelReverseMap(_contactsController.GetAllContacts().Result.Results.ToList());
            
            return await Task.Run(() => { return View(contactListModel); });
        }

        public async Task<IActionResult> Search(UserModel userModel)
        {
            var userFiltered = _mapper.Map<User>(userModel);
            var contactsListFiltered = _contactsController.GetContactsByFilters(userFiltered).Result.Results;
            var contactsListFilteredModel = MappingProfile.UserModelReverseMap(contactsListFiltered.ToList());

            return await Task.Run(() => { return View("Index", contactsListFilteredModel); });
        }

        public async Task<IActionResult> ClearRefresh()
        {
            var contactListModel = MappingProfile.UserModelReverseMap(_contactsController.GetAllContacts().Result.Results.ToList());

            return await Task.Run(() => { return View("Index", contactListModel); });
        }

        public async Task<IActionResult> EditContact(int idContact)
        {
            var contactsEmailListModel = MappingProfile.UniqueUserModelReverseMap(await _contactsController.GetFirstOrDefault(idContact));
            var teste = _emailListController.GetAllContactEmails(idContact).Result.Results;
            var teste2 = new List<EmailListModel>();

            foreach (var item in teste)
            {
                var teste3 = new EmailListModel();
                teste3.Id = item.Id;
                teste3.Email = item.Email;
                teste2.Add(teste3);
            }

            contactsEmailListModel.ContactsEmailList = teste2;

            return await Task.Run(() => { return View(contactsEmailListModel); });
        }

        public async Task<IActionResult> DeleteContact(int idContact)
        {
            var response = await _contactsController.DeleteContact(idContact);

            if (response.Success)
            {
                return RedirectToAction("Index", "ContactsList");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ViewContactsList(int idContact)
            => await Task.Run(() => RedirectToAction("Index", "ContactsList", new { idContact = idContact }));

        public async Task<IActionResult> SaveContact(ContactsEmailListModel contactsEmailListModel)
        {
            var contact = _mapper.Map<User>(contactsEmailListModel.ContactModel);
            var response = await _contactsController.UpdateContact(contact);
            return RedirectToAction("EditContact", "ContactsList", new { idContact = response.IdReturn } );
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEditContactEmails(int idContact, int idEmail, bool isMainEmail, string email)
        {
            var response = await _emailListController.UpdateEmail(idEmail, email);

            if (isMainEmail)
                await _contactsController.SetMainEmailContact(idContact, email);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CheckMainEmail(int idContact, string email)
        {
            var isMainEmail = await _contactsController.CheckMainEmail(idContact, email);
            return Json(isMainEmail);
        }

        public async Task<IActionResult> GetEmailIdByEmail(string email)
        {
            var emailId = await _emailListController.GetEmailIdByEmail(email);
            return Json(emailId);
        }
    }
}
