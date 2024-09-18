using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;
using System.Threading.Tasks;

namespace Surfs_Up.Controllers {

    public class BookingController : Controller 
    {
        private readonly AppDbContext _dbContext;
        public BookingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            var items = cart.GetCartItems();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            booking.BookingItems = cart.GetCartItems();

            if (booking.BookingItems == null || !booking.BookingItems.Any())
            {
                ModelState.AddModelError("BookingItems", "Kurven er tom!");
            }

            if (ModelState.IsValid)
            {
                //Customer customer = booking.Customer;
                //await AddCustomer(customer);
                foreach (var item in booking.BookingItems)
                {
                    if (_dbContext.CatalogItems.Any(c => c.CatalogItemId == item.CatalogItemId))
                    {
                        _dbContext.Attach(item);
                    } 
                }
                    await AddBooking(booking);
                return RedirectToAction("BookingSuccess");
            }
            
            return View("Index", cart.GetCartItems());
        }

        public async Task AddCustomer(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddBooking(Booking booking)
        {
            await _dbContext.Bookings.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
        }

        public IActionResult BookingSuccess()
        {
            return View("BookingSuccess");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            var item = cart.GetCartItems().FirstOrDefault(item => item.CatalogItemId == id);

            if(item != null) 
            {
                cart.RemoveFromCart(item);
                return RedirectToAction("Index");
            }
            
            return NotFound();
        }
    }
}