using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IRoomRepository
    {
        ICollection<Room> GetRooms();
        Room GetRoom (int id);
        Room GetRoom (string  Roomtype);

        bool RoomExist (int ID);
        bool CreateRoom(Room room);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);

        bool save();

    }
}
