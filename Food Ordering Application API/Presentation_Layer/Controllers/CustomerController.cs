using Business_Layer.Services;
using Data_Accesss_Layer.DTOs;
using Data_Accesss_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Customer,RestaurantOwner")]
        [HttpGet("browseRestaurants")]
        public async Task<IActionResult> BrowseRestaurants(string search)
        {
            var restaurants = await _customerService.BrowseRestaurantsAsync(search);
            return Ok(restaurants);
        }

        [Authorize(Roles = "Customer,RestaurantOwner")]
        [HttpGet("viewMenu/{restaurantId}")]
        public async Task<IActionResult> ViewMenu(string restaurantId)
        {
            var menuItems = await _customerService.ViewMenuAsync(restaurantId);
            return Ok(menuItems);
        }


        // Backend - C# Controller
        [Authorize(Roles = "Customer")]
        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                var result = await _customerService.AddToCartAsync(
                    addToCartDto.CustomerId,
                    addToCartDto.MenuId,
                    addToCartDto.Quantity
                );

                return Ok(new { message = "Item added to cart successfully!" }); // return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /*[HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder(string customerId)
        {
            var result = await _customerService.PlaceOrderAsync(customerId);
            return Ok(new { message = "Ordered successfully!" });
        }*/
        [HttpPost("placeOrder")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PlaceOrder(string customerId)
        {
            var orderDetailsDto = await _customerService.PlaceOrderAsync(customerId);

            if (orderDetailsDto == null)
            {
                return BadRequest(new { message = "Cart is empty or some cart items have missing data." });
            }

            return Ok(new
            {
                message = "Order placed successfully.",
                orderDetails = orderDetailsDto
            });
        }

        [HttpGet("GetOrderHistory/{orderId}")]
        public async Task<IActionResult> GetOrderHistory(string orderId)
        {
            var orderDetails = await _customerService.GetOrderHistoryByOrderIdAsync(orderId);

            if (orderDetails == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            return Ok(orderDetails);
        }


        [HttpGet("trackOrder/{orderId}")]
        public async Task<IActionResult> TrackOrder(string orderId)
        {
            var orderStatus = await _customerService.TrackOrderAsync(orderId);
            if (orderStatus == null) return NotFound("Order not found.");

            return Ok(orderStatus);
        }

        [HttpGet("orderHistory/{customerId}")]
        public async Task<IActionResult> OrderHistory(string customerId)
        {
            var orders = await _customerService.OrderHistoryAsync(customerId);
            return Ok(orders);
        }

        [HttpPost("{orderId}/AddReview")]
        public async Task<IActionResult> AddReview(string orderId, [FromBody] Review review)
        {
            var result = await _customerService.AddReviewAsync(orderId, review);
            return Ok(result);
        }
    }
}
