using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IReservationRepository
    {
        ICollection<Reservation>  GetReservations();
        Reservation GetReservation (int id);
        Reservation GetReservation (string  Roomtype);

        bool ReservationExist (int RoomID);
        bool CreateReservation(Reservation reservation);
        bool UpdateReservation(Reservation reservation);
        bool DeleteReservation(Reservation reservation);

        bool save();

    }
}
