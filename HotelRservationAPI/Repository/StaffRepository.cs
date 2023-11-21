using HotelRservationAPI.Data;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;

namespace HotelRservationAPI.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DataContext _context;

        public StaffRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateStaff(Staff staff)
        {
            _context.Add(staff);
            return save();
        }

        public bool DeleteStaff(Staff staff)
        {
            _context.Remove(staff);
            return save();
        }

        public Staff GetStaff(int id)
        {
            return _context.staff.Where(a => a.StaffID == id).FirstOrDefault();
        }

        public Staff GetStaff(string StaffName)
        {
            return _context.staff.Where(a => a.StaffName == StaffName).FirstOrDefault();
        }

        public ICollection<Staff> GetStaff()
        {
            return _context.staff.OrderBy(s => s.StaffID).ToList();
        }

       

        public bool StaffExist(int ID)
        {
            return _context.staff.Any(a => a.StaffID == ID);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateStaff(Staff staff)
        {
            _context.Update(staff);
            return save();
        }
    }
}
