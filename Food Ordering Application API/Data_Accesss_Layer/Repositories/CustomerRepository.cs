using Data_Accesss_Layer.Data;
using Data_Accesss_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> BrowseRestaurantsAsync(string search)
        {
            return await _context.Restaurants
                                 .Where(r => r.Name.Contains(search))
                                 .ToListAsync();
        }

       

        public async Task<Cart> AddToCartAsync(Cart cartItem)
        {
            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<List<Cart>> GetCartItemsAsync(string customerId)
        {
            return await _context.Carts
                                 .Include(c => c.Menu)
                                 .Where(c => c.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<Order> PlaceOrderAsync(Order order, List<Cart> cartItems)
        {
            _context.Orders.Add(order);
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<List<Menu>> ViewMenuAsync(string restaurantId)
        {
            return await _context.Menus.Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<Order> TrackOrderAsync(string orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task<List<Order>> OrderHistoryAsync(string customerId)
        {
            return await _context.Orders
                                 .Where(o => o.CustomerId == customerId)
                                 .ToListAsync(); 
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<List<OrderHistory>> GetOrderHistoryByOrderIdAsync(string orderId)
        {
            return await _context.OrderHistories
                .Include(oh => oh.Menu)
                .Where(oh => oh.OrderId == orderId)
                .ToListAsync();
        }
    }
}
