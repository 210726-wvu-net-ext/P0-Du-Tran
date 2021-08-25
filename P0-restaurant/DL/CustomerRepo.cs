using Models;
using DL.Entities;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DL
{
public class CustomerRepo : ICustomerRepo
{
    private P0_RestaurantRContext _context;
    public CustomerRepo(P0_RestaurantRContext context) => _context = context;
    public List<Models.Customer> GetAllCustomers()
    {
        return _context.Customers.Select(
            customer => new Models.Customer(customer.Id, customer.FirstName, customer.LastName, customer.UserName, customer.Email)
        ).ToList();
    }

    public Models.Customer AddAUser(Models.Customer customer)
    {
        _context.Customers.Add(
            new Entities.Customer {
                FirstName=customer.FirstName,
                LastName=customer.LastName,
                UserName=customer.UserName,
                Email=customer.Email
            }
        );
        _context.SaveChanges();
        
        return customer;
    }

    public Models.Customer searchUsersByName(string firstname)
    {
        Entities.Customer foundCustomer = _context.Customers
            .FirstOrDefault(customer => customer.FirstName == firstname);
        if(foundCustomer != null)
        {
            return new Models.Customer(foundCustomer.Id, foundCustomer.FirstName, foundCustomer.LastName, foundCustomer.UserName, foundCustomer.Email);
        }
        return new Models.Customer();
    }
}
}