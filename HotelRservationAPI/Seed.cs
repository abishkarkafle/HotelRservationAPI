using HotelRservationAPI.Data;
using HotelRservationAPI.Models;

namespace HotelRservationAPI
{
    public class Seed
    {

        private DataContext Context;
        public Seed( DataContext _context)
        {
            Context = _context;
        }

        public void SeedDataContext()
        {
            if (!Context.rooms.Any())
            {
                var RoomSeed = new List<Room>
                {
                    new Room
                    {
                            RoomType = "Single",
                            PricePerNight = 150,
                            AvailableCount = 5
                    },
                    new Room
                    {
                        RoomType = "Double",
                        PricePerNight = 300,
                        AvailableCount = 8

                    }
                };
                Context.rooms.AddRange(RoomSeed);

                var ReservationSeed = new List<Reservation>
                {
                    new Reservation
                    {
                        RoomType = "Single",
                        CheckinDate = DateTime.Now,
                        CheckoutDate = DateTime.Now,
                    }
                };
                Context.reservations.AddRange(ReservationSeed);
            }
        }
    }
}
