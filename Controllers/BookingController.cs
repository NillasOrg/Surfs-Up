using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;
using Surfs_Up.Repository;

namespace Surfs_Up.Controllers {

    public class BookingController : Controller 
    {
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
                bookingRepo.SaveBookingToTextFile(booking);
                return RedirectToAction("BookingSuccess");
            }
            
            return View("Index", cart.GetCartItems());
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