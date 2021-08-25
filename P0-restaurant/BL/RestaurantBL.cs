using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class RestaurantBL : IRestaurantBL
    {
        private IRestaurantRepo _repo;
        public RestaurantBL(IRestaurantRepo repo)
        {
            _repo = repo;
        }
        public Restaurant FindARestaurant(string name)
        {
            return _repo.FindARestaurant(name);
        }
        public List<Restaurant> ViewAllRestaurants()
        {
            return _repo.GetAllRestaurants();
        }
        public Restaurant FindARestaurantById(int id)
        {
            return _repo.FindARestaurantById(id);
        }
        public Restaurant FindARestaurantByZipcode(string zipcode)
        {
            return _repo.FindARestaurantByZipcode(zipcode);
        }

    }
}