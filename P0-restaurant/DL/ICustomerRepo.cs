using Models;

namespace DL
{
    public interface ICustomerRepo
    {
       List<Customer> GetAllCustomers();
       Customer AddAUser(Customer customer);
       Customer searchUsersByName(string lastname);

    }
}