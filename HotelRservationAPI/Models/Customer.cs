namespace HotelRservationAPI.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerReview { get; set; }

        public ICollection<Reservation> reservation { get; set; }
    }
}
