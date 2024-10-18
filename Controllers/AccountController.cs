using System.Security.Claims;
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
            var isLoggedIn = await _service.Login(model);

            if (isLoggedIn != null)
            {
                var response = await _service.Login(model);
                // Det er her magien sker!
                HttpContext.Session.SetString("AccessToken", response.AccessToken);
                HttpContext.Session.SetString("RefreshToken", response.RefreshToken);
                HttpContext.Session.SetString("Email", model.Email); // En user har unik email, så vi gemmer email i session, så den kan sendes til API.

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            _service.Logout();

            if (!await _service.isLoggedIn())
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}