using AutoMapper;
using ContactsWeb.Models;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Diagnostics;

namespace ContactsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactController _contactController;
        private readonly IMapper _mapper;

        public HomeController(IContactController contactController, IMapper mapper)
        {
            this._contactController = contactController;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => { return View(); });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterContact(ContactModel userModel)
        {
            var createdUser = _mapper.Map<Contact>(userModel);
            var response = await _contactController.RegisterContact(createdUser);

            if (response.Success)
            {
                return RedirectToAction("Index", "EmailRegister", new { contactId = response.IdReturn });
            }
            return View("Index");
        }

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}