namespace DoctorsPortalServer.NETWebAPI.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string email { get; set; }
        public int BookingId { get; set; }
    }
}
