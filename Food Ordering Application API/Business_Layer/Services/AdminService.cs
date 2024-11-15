using Data_Accesss_Layer.Models;
using Data_Accesss_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<string> ManageUserAccountAsync(string userId, bool isActive)
        {
            if (userId == null) throw new ArgumentException("Invalid user ID.");

            try
            {
                var user = await _adminRepository.GetUserByIdAsync(userId);
                if (user == null) return "User not found.";

                user.IsActive = isActive;
                await _adminRepository.SaveChangesAsync();

                return "User account status updated.";
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception("An error occurred while managing user account.", ex);
            }
        }

        public async Task<string> ManageRestaurantAsync(string restaurantId, bool isActive)
        {
            if (restaurantId ==null) throw new ArgumentException("Invalid restaurant ID.");

            try
            {
                var restaurant = await _adminRepository.GetRestaurantByIdAsync(restaurantId);
                if (restaurant == null) return "Restaurant not found.";

                restaurant.IsActive = isActive;
                await _adminRepository.SaveChangesAsync();

                return "Restaurant status updated.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while managing restaurant.", ex);
            }
        }

        public async Task<string> ResolveDisputeAsync(string orderId, string resolution)
        {
            if (orderId == null) throw new ArgumentException("Invalid order ID.");

            try
            {
                var order = await _adminRepository.GetOrderByIdAsync(orderId);
                if (order == null) return "Order not found.";

                order.DisputeResolution = resolution;
                order.Status = "Resolved";
                await _adminRepository.SaveChangesAsync();

                return "Dispute resolved successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while resolving dispute.", ex);
            }
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            try
            {
                return await _adminRepository.GetAllRestaurantsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving restaurants.", ex);
            }
        }
    }
}
