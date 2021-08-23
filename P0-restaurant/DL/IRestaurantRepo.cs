using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRestaurantRepo
    {
         Restaurant FindARestaurant(string name);
         List<Restaurant> GetAllRestaurants();
    }
}