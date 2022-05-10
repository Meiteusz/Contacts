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

        public async Task<IActionResult> Index(int idContact)
        {
            var emailContactsList = _emailListController.GetAllContactEmails(idContact).Result.Results;
            var emailListModel = new List<EmailListModel>();

            foreach (var emailContact in emailContactsList)
            {
                emailListModel.Add(new EmailListModel() { Email = emailContact.Email });
            }

            return await Task.Run(() => { return View(emailListModel); });
        }

        public async Task<IActionResult> RegisterEmail(int id)
            => await Task.Run(() => { return RedirectToAction("Index", "EmailRegister", new { idContact = id }); });
    }
}
