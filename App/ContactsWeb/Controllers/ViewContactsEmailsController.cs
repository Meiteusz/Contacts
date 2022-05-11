using ContactsWeb.Models;
using Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ContactsWeb.Controllers
{
    public class ViewContactsEmailsController : Controller
    {
        private readonly IEmailListController _emailListController;

        public ViewContactsEmailsController(IEmailListController emailListController)
        {
            this._emailListController = emailListController;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int contactId)
        {
            var contactsEmailList = _emailListController.GetAllContactEmails(contactId).Result.Results;
            var emailListModel = new List<EmailListModel>();

            foreach (var contactEmail in contactsEmailList)
            {
                emailListModel.Add(new EmailListModel() { Email = contactEmail.Email });
            }

            return await Task.Run(() => { return View(emailListModel); });
        }

        [HttpGet]
        public async Task<IActionResult> RegisterEmail(int contactId)
            => await Task.Run(() => { return RedirectToAction("Index", "EmailRegister", new { contactId = contactId }); });
    }
}
