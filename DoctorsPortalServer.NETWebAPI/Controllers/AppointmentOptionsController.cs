using DoctorsPortalServer.NETWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsPortalServer.NETWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AppointmentOptionsController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentOptionsController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAppointmentOptions([FromQuery] string date)
        {
            var Options = await appointmentRepository.GetAllAppointmentOptionsAsync(date);
            return Ok(Options);
        }
    }
}
