using Models;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DL
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private P0_RestaurantRContext _context;
        public RestaurantRepo(P0_RestaurantRContext context)
        {
            _context = context;
        }
        public Models.Restaurant  FindARestaurant(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Name == name);
            if(foundRestaurant != null)
            {
                return new Models.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Contact);
            }
            return new Models.Restaurant();
        }
        public List<Models.Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.Select(
                restaurant => new Models.Restaurant(restaurant.Name)
            ).ToList();
        }
    }
}