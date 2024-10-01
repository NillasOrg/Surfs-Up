using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Data.Services;
using Surfs_Up.Models;
using Surfs_Up.ViewModels;

namespace Surfs_Up.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _service;

        public AccountController()
        {
            _service = new UserService();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return View(model);
        }
        public IActionResult Login() 
        { 
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            bool isLoggedIn = await _service.Login(model.UserNameOrEmail, model.Password);

            if (isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            return RedirectToAction("Login");
        }

    }
}
