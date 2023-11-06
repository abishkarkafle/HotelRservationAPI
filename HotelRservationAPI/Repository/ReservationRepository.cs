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

        public bool CreateReservation(Reservation reservation)
        {
            _context.Add(reservation);
            return save();
        }

        public bool DeleteCategory(Reservation reservation)
        {
            _context.Remove(reservation);
            return save();
        }

        public Reservation GetReservation(int id)
        {
            return _context.reservations.Where(a => a.ReservationID == id).FirstOrDefault();
        }

        public Reservation GetReservation(string Roomtype)
        {
            return _context.reservations.Where(a => a.RoomType == Roomtype).FirstOrDefault();
        }

        public ICollection<Reservation> GetReservations()
        {
            return _context.reservations.OrderBy(s => s.ReservationID).ToList();
        }

        public bool ReservationExist(int ID)
        {
            return _context.reservations.Any(a => a.ReservationID == ID);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            _context.Update(reservation);
            return save();
        }
    }
}
