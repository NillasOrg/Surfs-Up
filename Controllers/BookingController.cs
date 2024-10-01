
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;
using Surfs_Up.Repository;

using System.Threading.Tasks;

namespace Surfs_Up.Controllers {

    public class BookingController : Controller 
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public BookingController(AppDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

     

        public async Task AddBooking(Booking booking)
        {
            await _dbContext.Bookings.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
        }

        public IActionResult Index()
        {

            ShoppingCart cart = ShoppingCart.GetInstance();
            var items = cart.GetCartItems();
            Booking booking = new Booking()
            {
                Surfboards = items
            };
            return View(booking);
        }

        [HttpPost]

        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            booking.Surfboards = cart.GetCartItems();

            if (booking.Surfboards == null || !booking.Surfboards.Any())
            {
                ModelState.AddModelError("Surfboards", "Kurven er tom!");
            }

            if (ModelState.IsValid)
            {

                foreach (var item in booking.Surfboards)
                {
                    if (_dbContext.Surfboards.Any(c => c.SurfboardId == item.SurfboardId))
                    {
                        _dbContext.Attach(item);
                    } 
                }

                 User user = await _userManager.GetUserAsync(User); 
            if (user != null)
            {
                booking.User = user;
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }


                await AddBooking(booking);
                return RedirectToAction("BookingSuccess", booking);
            }
            
            return View("Index", booking);
        }

        public IActionResult BookingSuccess(Booking booking, int bookingId)
        {
            booking = _dbContext.Bookings
                   .Include(b => b.Surfboards) 
                   .Include(b => b.User)      
                   .FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
            {
                return NotFound();
            }
           

            return View("BookingSuccess", booking);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            ShoppingCart cart = ShoppingCart.GetInstance();
            var item = cart.GetCartItems().FirstOrDefault(item => item.SurfboardId == id);

            if(item != null) 
            {
                cart.RemoveFromCart(item);
                return RedirectToAction("Index");
            }
            
            return NotFound();
        }
    }
}