using Models;
using System.Collections.Generic;

namespace BL
{

public interface ICustomerBL
{
    List<Customer> ViewAllCustomers();
    Customer AddAUser(Customer customer);
    Customer searchUsersByName(string lastname);
}

}
