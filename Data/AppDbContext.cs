using Microsoft.EntityFrameworkCore;
using DamasBarbershop.Models;

namespace DamasBarbershop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Booking> Bookings => Set<Booking>();
}
