using HotelRservationAPI.Models;

namespace HotelRservationAPI.Interface
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetCustomer();
        Customer GetCustomer(int CustomerID);
        Customer GetCustomer(string CustomerName);

        bool CustomerExist(int CustomerID);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);

        bool save();
    }
}
