using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;

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
            return View();
        }

        public IActionResult Registratrion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registratrion(RegistrationVeiwModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.Email = model.Email;
                customer.Name = model.Name;
                customer.UserName = model.UserName;
                customer.Password = model.Password;

                _context.Customers.Add(customer);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = $"{customer.Name} {customer.Email} {customer.Password} er oprettet";

                return View();
            }
            return View(model);
        }
    }
}
