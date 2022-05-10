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
        private readonly IUserController _userController;
        private readonly IMapper _mapper;

        public HomeController(IUserController userController, IMapper mapper)
        {
            this._userController = userController;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => { return View(); });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterContact(UserModel userModel)
        {
            var userCreated = _mapper.Map<User>(userModel);
            userCreated.MainEmail = string.Empty; //tirar dps
            var response = await _userController.RegisterUser(userCreated);

            if (response.Success)
            {
                return RedirectToAction("Index", "EmailRegister", new { id = response.IdReturn });
            }
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}