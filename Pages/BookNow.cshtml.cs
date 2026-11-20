using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DamasBarbershop.Data;
using DamasBarbershop.Models;

namespace DamasBarbershop.Pages;

public class BookNowModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public BookNowModel(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    [BindProperty]
    public Booking Booking { get; set; } = new();

    public bool Success { get; set; }

    public record ServiceOption(string Name, string Price, string Time);
    public record BarberOption(string Name, string Role);

    public ServiceOption[] Services { get; } = new[]
    {
        new ServiceOption("Signature Haircut", "$35", "30 min"),
        new ServiceOption("Skin Fade", "$40", "40 min"),
        new ServiceOption("Beard Trim & Shape", "$20", "15 min"),
        new ServiceOption("Hot Towel Shave", "$40", "45 min"),
        new ServiceOption("Haircut + Beard Trim", "$50", "45 min"),
        new ServiceOption("The Full Service", "$70", "75 min"),
    };

    public BarberOption[] Barbers { get; } = new[]
    {
        new BarberOption("No Preference", "First available barber"),
        new BarberOption("Barber Name", "Owner / Master Barber"),
        new BarberOption("Barber Name", "Senior Barber"),
        new BarberOption("Barber Name", "Barber"),
    };

    public List<string> Times { get; } = BuildTimes();

    private static List<string> BuildTimes()
    {
        var list = new List<string>();
        for (int h = 9; h <= 18; h++)
        {
            foreach (var m in new[] { "00", "30" })
            {
                if (h == 18 && m == "30") continue;
                int hr12 = h > 12 ? h - 12 : h;
                string ampm = h >= 12 ? "PM" : "AM";
                list.Add($"{hr12}:{m} {ampm}");
            }
        }
        return list;
    }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Booking.Service) || string.IsNullOrWhiteSpace(Booking.Barber) ||
            string.IsNullOrWhiteSpace(Booking.Date) || string.IsNullOrWhiteSpace(Booking.Time) ||
            string.IsNullOrWhiteSpace(Booking.Name) || string.IsNullOrWhiteSpace(Booking.Phone))
        {
            ModelState.AddModelError(string.Empty, "Missing required fields.");
            return Page();
        }

        Booking.SubmittedAt = DateTime.UtcNow;
        _db.Bookings.Add(Booking);
        _db.SaveChanges();

        // Email sending is not wired up yet — plug in an email provider (e.g. SendGrid, SMTP)
        // here using _config["ShopInfo:Email"] as the notification recipient.

        Success = true;
        return Page();
    }
}
