namespace HotelRservationAPI.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public string PricePerNight { get; set;}
        public int AvailableCount { get; set; }
    }
}
