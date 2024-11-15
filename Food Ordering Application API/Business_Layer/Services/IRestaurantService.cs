
using Data_Accesss_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IRestaurantService
    {
        Restaurant RegisterRestaurant(string ownerId, string name, string location, string cuisine);
        Menu AddMenuItem(string restaurantId, string name, decimal price);

        Menu GetMenuItemById(string menuId);
        Menu EditMenuItem(string menuId, string name, decimal price);
        void DeleteMenuItem(string menuId);
        List<Review> ViewReviews(string restaurantId);
        void RespondToReview(string reviewId, string response);
    }
}
