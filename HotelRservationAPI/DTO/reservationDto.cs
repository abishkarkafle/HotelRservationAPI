﻿namespace HotelRservationAPI.DTO
{
    public class ReservationDto
    {
        public int ReservationID { get; set; }
        public String RoomType { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
    }
}
