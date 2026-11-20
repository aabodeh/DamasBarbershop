namespace DamasBarbershop.Models;

public class Booking
{
    public int Id { get; set; }
    public string Service { get; set; } = "";
    public string Barber { get; set; } = "";
    public string Date { get; set; } = "";
    public string Time { get; set; } = "";
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string? Email { get; set; }
    public string? Notes { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}
