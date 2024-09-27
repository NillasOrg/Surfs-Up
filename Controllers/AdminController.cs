using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Surfs_Up.Models;

namespace Surfs_Up.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _dbContext;
    
    public AdminController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IActionResult> Index()
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.BookingItems) 
            .Include(b => b.User)      
            .ToListAsync();
        return View(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBooking(int id)
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