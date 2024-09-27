using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using System.Security.Claims;

namespace Surfs_Up.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IActionResult Index()
        {
            return View(_context.Customers.ToList());
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationVeiwModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.Email = model.Email;
                customer.Name = model.Name;
                customer.Password = model.Password;

                try
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{customer.Name} {customer.Email} {customer.Password} er oprettet";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Indtast venligst et unikt Email eller adgangskode");
                    return View(model);
             
                }

                return View();
            }
            return View(model);
        }
        public IActionResult Login() 
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Customers.Where(x => (x.Email == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault();
                if (user != null) 
                {
                    //Success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.Name),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Brugernavn/Email eller Adgangskode er ikke korrekt");
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

    }
}
