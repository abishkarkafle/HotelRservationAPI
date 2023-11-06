using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IReservationRepository
    {
        ICollection<Reservation>  GetReservations();
    }
}
