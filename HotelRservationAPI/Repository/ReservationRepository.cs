using HotelRservationAPI.Data;
using HotelRservationAPI.Models;

namespace HotelRservationAPI.Repository
{
    public class ReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Reservation> GetBillings()
        {
            return _context.reservations.OrderBy(s => s.ReservationID).ToList();
        }
    }
}
