using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Core;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        // You can also define other DbSets for your other models
        // public DbSet<Room> Rooms { get; set; }
        // public DbSet<Customer> Customers { get; set; }
    }
}