using HotelRservationAPI.Data;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;

namespace HotelRservationAPI.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRoom(Room room)
        {
            _context.Add(room);
            return save();
        }

        public bool DeleteRoom(Room room)
        {
            _context.Remove(room);
            return save();
        }

        public Room GetRoom(int id)
        {
            return _context.rooms.Where(a => a.RoomID == id).FirstOrDefault();
        }

        public Room GetRoom(string Roomtype)
        {
            return _context.rooms.Where(a => a.RoomType == Roomtype).FirstOrDefault();
        }

        public ICollection<Room> GetRooms()
        {
            return _context.rooms.OrderBy(s => s.RoomID).ToList();
        }

       

        public bool RoomExist(int ID)
        {
            return _context.rooms.Any(a => a.RoomID == ID);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRoom(Room room)
        {
            _context.Update(room);
            return save();
        }
    }
}
