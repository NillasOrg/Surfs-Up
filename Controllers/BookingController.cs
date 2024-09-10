using Microsoft.AspNetCore.Mvc;
using Surfs_Up.Models;
using Surfs_Up.Repository;

namespace Surfs_Up.Controllers {

    public class BookingController : Controller 
    {
        public IActionResult Index()
        {
            var items = ItemList.GetList();
            return View(items);
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking booking)
        {
            var items = ItemList.GetList();
            CatalogItem? catalogItem = items.FirstOrDefault(item => item.CatalogItemId == booking.CatalogItem.CatalogItemId);
            booking.CatalogItem = catalogItem;

            if (ModelState.IsValid)
            {
                BookingRepo bookingRepo = new BookingRepo();
                bookingRepo.SaveBookingToTextFile(booking);

                return RedirectToAction("BookingSuccess");
            }

            return View("Index");
        }

        public IActionResult BookingSuccess()
        {
            return View("BookingSuccess");
        }


    }
}