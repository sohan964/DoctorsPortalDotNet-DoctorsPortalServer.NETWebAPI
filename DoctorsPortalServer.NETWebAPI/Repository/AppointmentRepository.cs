using DoctorsPortalServer.NETWebAPI.Data;
using DoctorsPortalServer.NETWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DoctorsPortalServer.NETWebAPI.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DoctorsPortalContext context;

        public AppointmentRepository(DoctorsPortalContext context)
        {
            this.context = context;
        }

        //GetAllAppointments
        public async Task<List<AppointmentModel>> GetAllAppointmentOptionsAsync(string date)
        {
            Console.WriteLine(date);

            var alreadyBooked = await context.Bookings.Where(x => x.AppointmentDate == date).ToListAsync();

            var appointOptions = await context.AppointmentOptions.Select(x => new AppointmentModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                slots = x.slots,

            }).ToListAsync();
            
            foreach( var option in appointOptions )
            {
                var optionBooked = alreadyBooked.Where(book => book.Treatment == option.Name);
                var bookedSlots = optionBooked.Select(book => book.Slot).ToList();
                var remainingSlots = option.slots.Where(slot => !bookedSlots.Contains(slot.Slot)).ToList();
                option.slots = remainingSlots;
            }
            return appointOptions;
        }

    }
}
