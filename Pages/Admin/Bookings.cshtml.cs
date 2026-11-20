using Microsoft.AspNetCore.Mvc.RazorPages;
using DamasBarbershop.Data;
using DamasBarbershop.Models;

namespace DamasBarbershop.Pages.Admin;

public class BookingsModel : PageModel
{
    private readonly AppDbContext _db;
    public BookingsModel(AppDbContext db) { _db = db; }

    public List<Booking> Bookings { get; set; } = new();

    public void OnGet()
    {
        Bookings = _db.Bookings.OrderByDescending(b => b.SubmittedAt).ToList();
    }
}
