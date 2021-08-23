using System.Collections.Generic;

namespace Models
{
    public class Customer
    {
        public Customer() {}
        public Customer(string firstName, string lastName, string username, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email=email;
            this.UserName = username;
        }
        public Customer(int id, string firstName, string lastName, string username, string email) : this(firstName, lastName, email, username)
        {
            this.Id = id;
            
        }
        public int Id {get; set;}
        public string LastName {get;set;}
        public string FirstName {get; set;}
        public string Email{get; set;}
        public string UserName {get; set;}
        public List<Review> Reviews {get;set;}
        public List<Restaurant> Restaurants {get; set;}
    }

}