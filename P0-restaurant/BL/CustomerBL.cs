using Models;
using DL;
using System.Collections.Generic;
namespace BL
{
    public class CustomerBL : ICustomerBL
    {
        private ICustomerRepo _repo;
        public CustomerBL(ICustomerRepo repo)
        {
            _repo = repo;
        }
        public List<Customer> ViewAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer AddAUser(Customer customer)
        {
            return _repo.AddAUser(customer);
        }
        public Customer searchUsersByName(string lastname)
        {
            return _repo.searchUsersByName(lastname);
        }
    }
}