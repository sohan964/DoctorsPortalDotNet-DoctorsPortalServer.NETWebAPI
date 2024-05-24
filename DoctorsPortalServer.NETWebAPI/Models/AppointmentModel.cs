using DoctorsPortalServer.NETWebAPI.Data;

namespace DoctorsPortalServer.NETWebAPI.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<AppointmentSlot> slots { get; set; }
    }
}
