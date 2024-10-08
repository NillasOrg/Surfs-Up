
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;

using System.Threading.Tasks;
using Surfs_Up.Data.Services;

namespace Surfs_Up.Controllers
{

    public class BookingController : Controller
    {
        private readonly BookingService _service;
        private readonly UserService _userService;
        public BookingController()
        {
            _userService = new UserService();
            _service = new BookingService();
        }

        public IActionResult Index()
        {

            ShoppingCart cart = ShoppingCart.GetInstance();
            var wetsuits = cart.GetItemsOfType<Wetsuit>();
            var surfboards = cart.GetItemsOfType<Surfboard>();
            Booking booking = new Booking()
            {
                Wetsuits = wetsuits,
                Surfboards = surfboards
            };
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            booking.Surfboards = cart.GetItemsOfType<Surfboard>();
            booking.Wetsuits = cart.GetItemsOfType<Wetsuit>();

            if (booking.Surfboards == null || !booking.Surfboards.Any())
            {
                ModelState.AddModelError("Surfboards", "Kurven er tom!");
            }

            if (ModelState.IsValid)
            {
                if (await _userService.IsLoggedIn())

                    foreach (var item in booking.Surfboards)
                    {
                        User user = await _userService.GetUser();
                        booking.User = user;
                    }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                    return View("Index", booking);
                }

                var createdBooking = await _service.Create(booking);
                Console.WriteLine($"Booking ID: {createdBooking.Id}");
                return RedirectToAction("BookingSuccess", new { bookingId = createdBooking.Id });
            }
            return View("Index", booking);
        }

        public async Task<IActionResult> BookingSuccess(int bookingId)
        {
            var booking = await _service.GetById(bookingId);

            if (booking == null)
            {
                return NotFound();
            }


            return View("BookingSuccess", booking);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id, string itemType)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            if (itemType == "surfboard")
            {
                var surfboard = cart.GetItemsOfType<Surfboard>().FirstOrDefault(item => item.Id == id);
                if (surfboard != null)
                {
                    cart.RemoveFromCart(surfboard);
                    return RedirectToAction("Index");
                }
            }
            else if (itemType == "wetsuit")
            {
                var wetsuit = cart.GetItemsOfType<Wetsuit>().FirstOrDefault(item => item.WetsuitId == id);
                if (wetsuit != null)
                {
                    cart.RemoveFromCart(wetsuit);
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }

    }
}