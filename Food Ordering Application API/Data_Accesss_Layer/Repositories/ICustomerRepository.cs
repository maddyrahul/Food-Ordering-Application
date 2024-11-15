using Data_Accesss_Layer.Models;


namespace Data_Accesss_Layer.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Restaurant>> BrowseRestaurantsAsync(string search);
        Task<List<Menu>> ViewMenuAsync(string restaurantId);
        Task<Cart> AddToCartAsync(Cart cartItem);
        Task<List<Cart>> GetCartItemsAsync(string customerId);
        Task<Order> PlaceOrderAsync(Order order, List<Cart> cartItems);
        Task<Order> TrackOrderAsync(string orderId);
        Task<List<Order>> OrderHistoryAsync(string customerId);
        Task<Review> AddReviewAsync(Review review);

        Task<List<OrderHistory>> GetOrderHistoryByOrderIdAsync(string orderId);
    }
}
