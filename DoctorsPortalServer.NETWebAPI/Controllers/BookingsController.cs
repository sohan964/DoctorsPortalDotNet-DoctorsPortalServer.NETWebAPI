using DoctorsPortalServer.NETWebAPI.Models;
using DoctorsPortalServer.NETWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsPortalServer.NETWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBooking([FromBody] BookingsModel bookingsModel)
        {
            var Booking = await bookingRepository.AddBookingAsync(bookingsModel);
            return Ok(Booking);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBookingByEmail([FromQuery] string email)
        {
            var bookings = await bookingRepository.GetBookingsByEmailAsync(email);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById([FromRoute] int id)
        {
            var booking = await bookingRepository.GetBookingByIdAsync(id);
            return Ok(booking);
        }


    }
}
