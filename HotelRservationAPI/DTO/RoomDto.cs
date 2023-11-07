namespace HotelRservationAPI.DTO
{
    public class RoomDto
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public int PricePerNight { get; set; }
        public int AvailableCount { get; set; }
    }
}
