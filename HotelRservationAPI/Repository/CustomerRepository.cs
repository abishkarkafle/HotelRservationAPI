using HotelRservationAPI.Data;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.Win32;

namespace HotelRservationAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            return save();
        }

        public bool CustomerExist(int CustomerID)
        {
            return _context.reservations.Any(a => a.ReservationID == CustomerID);
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return save();
        }

        public ICollection<Customer> GetCustomer()
        {
            return _context.customers.OrderBy(s => s.CustomerID).ToList();
        }

        public Customer GetCustomer(int CustomerID)
        {
            return _context.customers.Where(a => a.CustomerID == CustomerID).FirstOrDefault();
        }

        public Customer GetCustomer(string CustomerName)
        {
            return _context.customers.Where(a => a.CustomerName == CustomerName).FirstOrDefault();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return save();
        }
    }
}

