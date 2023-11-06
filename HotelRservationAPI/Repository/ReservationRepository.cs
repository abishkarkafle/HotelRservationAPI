using HotelRservationAPI.Data;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;

namespace HotelRservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Reservation> GetReservations()
        {
            return _context.reservations.OrderBy(s => s.ReservationID).ToList();
        }
    }
}
