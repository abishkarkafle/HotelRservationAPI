using HotelRservationAPI.Data;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;

namespace HotelRservationAPI.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly DataContext _context;

        public RegisterRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRegister(Register register)
        {
            _context.Add(register);
            return save();
        }

        public bool DeleteRegister(Register register)
        {
            _context.Remove(register);
            return save();
        }

        public Register GetRegister(string username)
        {
            return _context.registers.Where(a => a.Username == username).FirstOrDefault();
        }

        public ICollection<Register> GetRegisters()
        {
            return _context.registers.OrderBy(s => s.Username).ToList();
        }

        public bool RegisterExist(string username)
        {
            return _context.registers.Any(a => a.Username == username);
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRegister(Register register)
        {
            _context.Update(register);
            return save();
        }
    }
}
