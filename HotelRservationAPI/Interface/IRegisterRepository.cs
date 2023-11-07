using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface IRegisterRepository
    {
        ICollection<Register> GetRegisters();
        Register GetRegister (string username);

        bool RegisterExist (string username);
        bool CreateRegister(Register register);
        bool UpdateRegister(Register register);
        bool DeleteRegister(Register register);

        bool save();

    }
}
