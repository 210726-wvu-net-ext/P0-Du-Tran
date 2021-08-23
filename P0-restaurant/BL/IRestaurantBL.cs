using Models;
using System.Collections.Generic;

namespace BL
{
    public interface IRestaurantBL
    {
        Restaurant FindARestaurant(string name);
        List<Restaurant> ViewAllRestaurants();
    }
}