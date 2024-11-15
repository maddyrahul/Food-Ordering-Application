using Data_Accesss_Layer.DTOs;
using Data_Accesss_Layer.Models;

namespace Business_Layer.Services
{
    public interface ICustomerService
    {
        // Browse and search restaurants
        Task<List<Restaurant>> BrowseRestaurantsAsync(string search);

        // View menu for a specific restaurant
        Task<List<Menu>> ViewMenuAsync(string restaurantId);

        // Add item to cart
        Task<string> AddToCartAsync(string customerId, string menuId, int quantity);

        // Place an order
        Task<OrderDetailsDto> PlaceOrderAsync(string customerId);

        // Track order status
        Task<object> TrackOrderAsync(string orderId);

        // View order history
        Task<List<object>> OrderHistoryAsync(string customerId);

        // Add review for a restaurant after an order
        Task<string> AddReviewAsync(string orderId, Review review);
        Task<OrderDetailsDto> GetOrderHistoryByOrderIdAsync(string orderId);

        // Task<List<OrderHistory>> GetOrderHistoryByOrderIdAsync(string orderId);
    }
}
