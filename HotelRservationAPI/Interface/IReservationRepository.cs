using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IReservationRepository
    {
        ICollection<Reservation>  GetReservations();
        Reservation GetReservation (int id);
        Reservation GetReservation (string  Roomtype);

        bool ReservationExist (int ID);
        bool CreateReservation(Reservation reservation);
        bool UpdateReservation(Reservation reservation);
        bool DeleteCategory(Reservation reservation);

        bool save();

    }
}
