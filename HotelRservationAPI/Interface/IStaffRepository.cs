using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IStaffRepository
    {
        ICollection<Staff> GetStaff();
        Staff GetStaff (int id);
        Staff GetStaff (string  StaffName);

        bool StaffExist (int StaffID);
        bool CreateStaff(Staff staff);
        bool UpdateStaff(Staff staff);
        bool DeleteStaff(Staff staff);

        bool save();

    }
}
