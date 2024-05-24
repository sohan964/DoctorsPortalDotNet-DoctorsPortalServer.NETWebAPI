using DoctorsPortalServer.NETWebAPI.Data;
using DoctorsPortalServer.NETWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace DoctorsPortalServer.NETWebAPI.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DoctorsPortalContext context;

        public BookingRepository(DoctorsPortalContext context)
        {
            this.context = context;
        }

        //postbooking
        public async Task<object> AddBookingAsync(BookingsModel bookingsModel)
        {
            Console.WriteLine(bookingsModel);
            var booking = new Bookings()
            {
                AppointmentDate = bookingsModel.AppointmentDate,
                Treatment = bookingsModel.Treatment,
                Patient = bookingsModel.Patient,
                Slot = bookingsModel.Slot,
                Email = bookingsModel.Email,
                Phone = bookingsModel.Phone,
                price = bookingsModel.price,
            };

            var alreadyBooked = await context.Bookings.Where(booked => booked.Email == bookingsModel.Email && booked.AppointmentDate == bookingsModel.AppointmentDate && booked.Treatment == bookingsModel.Treatment).ToListAsync();

            Console.WriteLine(alreadyBooked);
            if (alreadyBooked.Any())
            {

                var message = $"You already have a booking on {booking.AppointmentDate}";
                var acknowledged = new  { 
                    Message = message,
                    Success = false,
                };

                return acknowledged;
            }
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
            return booking;
        }

        //get bookings by email
        public async Task<List<BookingsModel>> GetBookingsByEmailAsync(string email)
        {
            var bookings = await context.Bookings.Where(x => x.Email == email).Select(x => new BookingsModel()
            {
                Id = x.Id,
                Email = x.Email,
                Phone = x.Phone,
                AppointmentDate = x.AppointmentDate,
                Treatment = x.Treatment,
                Patient = x.Patient,
                Slot = x.Slot,
                price = x.price,
            }).ToListAsync();
            return bookings;

        }
        
        //get Booking byId
        public async Task<Bookings> GetBookingByIdAsync(int id)
        {
            var booking = await context.Bookings.FindAsync(id);
            Console.WriteLine(booking);
            return booking;

        }

        //payment intant
        public async Task<int> StorePaymentAsync(PaymentModel paymentModel)
        {
            var Payment = new PaymentModel()
            {
                TransactionId = paymentModel.TransactionId,
                email = paymentModel.email,
                BookingId = paymentModel.BookingId,

            };
            context.PaymentModel.Add(Payment);
            await context.SaveChangesAsync();
            return Payment.Id;
            
        }
         
    }
}
