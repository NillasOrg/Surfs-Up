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

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var isRegistered = await _service.Register(model.Name, model.Email, model.Password);

            if (isRegistered)
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public IActionResult Login() 
        { 
            return View(); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var isLoggedIn = await _service.Login(model.UserNameOrEmail, model.Password);

            if (isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            _service.Logout();

            if (!await _service.IsLoggedIn())
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}