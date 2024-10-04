using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up.Models;

namespace Surfs_Up.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _dbContext;

    public AdminController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(int? deleteBookingId = null)
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.Surfboards) 
            .Include(b => b.Wetsuits)
            .Include(b => b.User)      
            .ToListAsync();

        // Pass the booking ID for deletion confirmation to the view
        ViewBag.DeleteBookingId = deleteBookingId;

        return View(bookings);
    }

    [HttpPost]
    public IActionResult ConfirmDeletionStep(int id)
    {
        // Redirect to index with a delete confirmation step
        return RedirectToAction("Index", new { deleteBookingId = id });
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDeleteBooking(int id)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.BookingId == id);

        if (booking != null)
        {
            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return NotFound();
    }
}