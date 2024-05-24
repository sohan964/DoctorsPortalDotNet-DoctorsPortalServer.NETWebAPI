using DoctorsPortalServer.NETWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorsPortalServer.NETWebAPI.Data
{
    public class DoctorsPortalContext : IdentityDbContext<ApplicationUser>
    {
        public DoctorsPortalContext(DbContextOptions<DoctorsPortalContext> options) : base(options) { }
        
        public DbSet<AppointmentOptions> AppointmentOptions { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<PaymentModel> PaymentModel { get; set; }
    }
}
