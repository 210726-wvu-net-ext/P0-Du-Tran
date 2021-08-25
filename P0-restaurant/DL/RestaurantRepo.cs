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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Models.Restaurant  FindARestaurant(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Name == name);
            if(foundRestaurant != null)
            {
                return new Models.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Zipcode, foundRestaurant.Contact);
            }
            return new Models.Restaurant();
        }

        public Models.Restaurant  FindARestaurantById(int id)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Id == id);
            if(foundRestaurant != null)
            {
                return new Models.Restaurant(foundRestaurant.Name, foundRestaurant.Zipcode);
            }
            return new Models.Restaurant();
        }
        public Models.Restaurant  FindARestaurantByZipcode(string zipcode)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Zipcode == zipcode);
            if(foundRestaurant != null)
            {
                return new Models.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Contact, foundRestaurant.Zipcode);
            }
            return new Models.Restaurant();
        }
        public List<Models.Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.Select(
                restaurant => new Models.Restaurant(restaurant.Id, restaurant.Name, restaurant.Location, restaurant.Contact, restaurant.Zipcode)
            ).ToList();
        }
    }
}