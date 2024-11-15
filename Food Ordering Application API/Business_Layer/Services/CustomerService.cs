using Data_Accesss_Layer.Data;
using Data_Accesss_Layer.DTOs;
using Data_Accesss_Layer.Models;
using Data_Accesss_Layer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerRepository _customerRepository;
        public CustomerService(ApplicationDbContext context, CustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository= customerRepository;
        }

        public async Task<List<Restaurant>> BrowseRestaurantsAsync(string search)
        {
            var restaurants = _context.Restaurants.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                restaurants = restaurants.Where(r => r.Name.Contains(search));
            }
            return await restaurants.ToListAsync();
        }

        public async Task<List<Menu>> ViewMenuAsync(string restaurantId)
        {
            return await _context.Menus.Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<string> AddToCartAsync(string customerId, string menuId, int quantity)
        {
            var cartItem = new Cart
            {
                CustomerId = customerId,
                MenuId = menuId,
                Quantity = quantity
            };

            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();

            return "Item added to cart.";
        }

        /*public async Task<string> PlaceOrderAsync(string customerId)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Menu)
                .ThenInclude(m => m.Restaurant)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();

            if (!cartItems.Any())
                return "Cart is empty.";

            if (cartItems.Any(c => c.Menu == null || c.Menu.Restaurant == null))
                return "Some cart items have missing menu or restaurant data.";

            var order = new Order
            {
                CustomerId = customerId,
                RestaurantId = cartItems.First().Menu.Restaurant.RestaurantId,
                OrderDate = DateTime.Now,
                Status = "Placed",
                TotalAmount = cartItems.Sum(c => c.Quantity * c.Menu.Price),
                DisputeResolution = "No"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Add entries to OrderHistory for each menu item in the cart
            var orderHistories = cartItems.Select(c => new OrderHistory
            {
                OrderId = order.OrderId,
                MenuId = c.Menu.MenuId
            }).ToList();

            _context.OrderHistories.AddRange(orderHistories);
            _context.Carts.RemoveRange(cartItems); // Clear cart after placing order
            await _context.SaveChangesAsync();

            return "Order placed successfully.";
        }
        */
        public async Task<OrderDetailsDto> PlaceOrderAsync(string customerId)
        {
            // Fetching the cart items for the customer
            var cartItems = await _context.Carts
                .Include(c => c.Menu)
                .ThenInclude(m => m.Restaurant)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();

            if (!cartItems.Any())
                return null;  // Cart is empty

            if (cartItems.Any(c => c.Menu == null || c.Menu.Restaurant == null))
                return null;  // Some cart items are missing menu or restaurant data

            // Create the order
            var order = new Order
            {
                CustomerId = customerId,
                RestaurantId = cartItems.First().Menu.Restaurant.RestaurantId,
                OrderDate = DateTime.Now,
                Status = "Placed",
                TotalAmount = cartItems.Sum(c => c.Quantity * c.Menu.Price),
                DisputeResolution = "No"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();  // Save the order to the database

            // Create OrderHistories for each item in the cart
            var orderHistories = cartItems.Select(c => new OrderHistory
            {
                OrderId = order.OrderId,
                MenuId = c.Menu.MenuId,
                Quantity = c.Quantity
            }).ToList();

            _context.OrderHistories.AddRange(orderHistories);
            _context.Carts.RemoveRange(cartItems);  // Clear cart after placing order
            await _context.SaveChangesAsync();

            // Fetch the created order and its associated order histories with menu details
            var orderDetails = await _context.Orders
                .Where(o => o.OrderId == order.OrderId)
                .Include(o => o.OrderHistories)
                    .ThenInclude(oh => oh.Menu)  // Include Menu details in OrderHistory
                .FirstOrDefaultAsync();

            // Mapping the Order and OrderHistory to OrderDetailsDto
            var orderHistoryDetails = orderDetails.OrderHistories.Select(oh => new OrderHistoryDto
            {
                MenuId = oh.Menu.MenuId,
                MenuName = oh.Menu.Name,
                MenuPrice = oh.Menu.Price
            }).ToList();

            var orderDetailsDto = new OrderDetailsDto
            {
                CustomerId = orderDetails.CustomerId,
                RestaurantId = orderDetails.RestaurantId,
                OrderDate = orderDetails.OrderDate,
                Status = orderDetails.Status,
                TotalAmount = orderDetails.TotalAmount,
                DisputeResolution = orderDetails.DisputeResolution,
                OrderHistory = orderHistoryDetails
            };

            return orderDetailsDto;  // Return the DTO with order and menu details
        }

        public async Task<object> TrackOrderAsync(string orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return null;

            return new { order.OrderDate, order.Status, order.TotalAmount };
        }

       /* public async Task<List<object>> OrderHistoryAsync(string customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Select(o => new { o.OrderDate, o.Status, o.TotalAmount })
                .ToListAsync<object>();
        }*/
        public async Task<List<object>> OrderHistoryAsync(string customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId) .ToListAsync<object>();
        }
        public async Task<string> AddReviewAsync(string orderId, Review review)
        {
            var order = await _context.Orders
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return "Order not found or not authorized.";

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.CustomerId == order.CustomerId && r.RestaurantId == order.RestaurantId);

            if (existingReview != null)
                return "Review already exists for this order.";

            review.CustomerId = order.CustomerId;
            review.RestaurantId = order.RestaurantId;
            review.Response = "";
            review.DatePosted = DateTime.UtcNow;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return "Review added successfully.";
        }

        public async Task<OrderDetailsDto> GetOrderHistoryByOrderIdAsync(string orderId)
        {
            // Fetch the order with related order histories and menu details
            var orderDetails = await _context.Orders
                .Where(o => o.OrderId == orderId)
                .Include(o => o.OrderHistories)
                    .ThenInclude(oh => oh.Menu)  // Include Menu details in OrderHistory
                .FirstOrDefaultAsync();

            if (orderDetails == null)
            {
                return null; // Handle case where order is not found
            }

            // Map OrderHistories to OrderHistoryDto
            var orderHistoryDetails = orderDetails.OrderHistories.Select(oh => new OrderHistoryDto
            {
                MenuId = oh.Menu.MenuId,
                MenuName = oh.Menu.Name,
                MenuPrice = oh.Menu.Price
            }).ToList();

            // Map the order details to OrderDetailsDto
            var orderDetailsDto = new OrderDetailsDto
            {
                CustomerId = orderDetails.CustomerId,
                RestaurantId = orderDetails.RestaurantId,
                OrderDate = orderDetails.OrderDate,
                Status = orderDetails.Status,
                TotalAmount = orderDetails.TotalAmount,
                DisputeResolution = orderDetails.DisputeResolution,
                OrderHistory = orderHistoryDetails
            };

            return orderDetailsDto;
        }

    }
}
