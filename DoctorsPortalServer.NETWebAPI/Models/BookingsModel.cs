namespace DoctorsPortalServer.NETWebAPI.Models
{
    public class BookingsModel
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; }
        public string Treatment { get; set; }
        public string Patient { get; set; }
        public string Slot { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int price { get; set; }
    }
}
