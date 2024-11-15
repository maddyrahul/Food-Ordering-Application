using Data_Accesss_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IAdminService
    {
       Task<string> ManageUserAccountAsync(string userId, bool isActive);
        Task<string> ManageRestaurantAsync(string restaurantId, bool isActive);
        Task<string> ResolveDisputeAsync(string orderId, string resolution);

        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    }
}
