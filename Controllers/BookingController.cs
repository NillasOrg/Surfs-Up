using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;

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
        public IActionResult CreateBooking(Booking booking)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            booking.BookingItems = cart.GetCartItems();

            if (booking.BookingItems == null || !booking.BookingItems.Any())
            {
                ModelState.AddModelError("BookingItems", "Kurven er tom!");
            }

            if (ModelState.IsValid)
            {
                BookingRepo bookingRepo = new BookingRepo();
                //bookingRepo.SaveBookingToTextFile(booking);
                AddBooking(booking).Wait();
                return RedirectToAction("BookingSuccess");
            }
            
            return View("Index", cart.GetCartItems());
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