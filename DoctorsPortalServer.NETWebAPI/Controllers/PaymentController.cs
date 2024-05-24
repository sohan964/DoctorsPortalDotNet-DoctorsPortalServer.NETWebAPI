using DoctorsPortalServer.NETWebAPI.Models;
using DoctorsPortalServer.NETWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Cryptography.X509Certificates;

namespace DoctorsPortalServer.NETWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;

        public PaymentController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> MakePayment([FromBody] int bookingPrice)
        {
            Console.WriteLine(bookingPrice);

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = bookingPrice*100,
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });
            
            return Ok(new { clientSecret  = paymentIntent.ClientSecret });
        }

        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody] PaymentModel paymentModel)
        {
            var paymentDone = await bookingRepository.StorePaymentAsync(paymentModel);

            Console.WriteLine(paymentDone);
            return Ok(paymentDone);
        }
    }
}
