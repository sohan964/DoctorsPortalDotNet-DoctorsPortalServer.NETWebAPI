namespace DoctorsPortalServer.NETWebAPI.Data
{
    public class AppointmentOptions
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<AppointmentSlot> slots { get; set; }
    }
}
