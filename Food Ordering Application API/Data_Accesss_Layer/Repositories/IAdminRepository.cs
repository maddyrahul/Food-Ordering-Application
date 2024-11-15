using Data_Accesss_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Repositories
{
    public interface IAdminRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<Restaurant> GetRestaurantByIdAsync(string restaurantId);
        Task<Order> GetOrderByIdAsync(string orderId);
        Task SaveChangesAsync();

        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    }
}
