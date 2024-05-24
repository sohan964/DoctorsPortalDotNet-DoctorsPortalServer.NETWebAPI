using DoctorsPortalServer.NETWebAPI.Models;

namespace DoctorsPortalServer.NETWebAPI.Repository
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentModel>> GetAllAppointmentOptionsAsync(string date);
        
    }
}
