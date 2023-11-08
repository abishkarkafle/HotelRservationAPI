using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IRegisterRepository
    {
        ICollection<Register> GetRegisters();

        bool RegisterExist (string Username);
        bool CreateRegister(Register register);

        bool save();

    }
}
