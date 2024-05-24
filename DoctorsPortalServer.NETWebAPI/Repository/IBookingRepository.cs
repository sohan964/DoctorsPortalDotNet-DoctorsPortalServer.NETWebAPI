using DoctorsPortalServer.NETWebAPI.Data;
using DoctorsPortalServer.NETWebAPI.Models;

namespace DoctorsPortalServer.NETWebAPI.Repository
{
    public interface IBookingRepository
    {
        Task<object> AddBookingAsync(BookingsModel bookingsModel);
        Task<List<BookingsModel>> GetBookingsByEmailAsync(string email);
        Task<Bookings> GetBookingByIdAsync(int id);
       Task<int> StorePaymentAsync(PaymentModel paymentModel);
    }
}
