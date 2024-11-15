using Business_Layer.Services;
using Data_Accesss_Layer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

       // Manage user account access levels
        [HttpPost("manageUserAccount")]
        public async Task<IActionResult> ManageUserAccount(string userId, bool isActive)
        {
            if (userId == null) return BadRequest("Invalid user ID.");

            try
            {
                var result = await _adminService.ManageUserAccountAsync(userId, isActive);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while managing user account.");
            }
        }
        // Manage restaurants
        [HttpPost("manageRestaurant")]
        public async Task<IActionResult> ManageRestaurant(string restaurantId, bool isActive)
        {
            if (restaurantId == null) return BadRequest("Invalid restaurant ID.");

            try
            {
                var result = await _adminService.ManageRestaurantAsync(restaurantId, isActive);
                return Ok(new { message = "Updated successfully!" }); // return JSON

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while managing the restaurant.");
            }
        }

        // Resolve disputes
        [HttpPost("resolveDispute")]
        public async Task<IActionResult> ResolveDispute(string orderId, string resolution)
        {
            if (orderId == null) return BadRequest("Invalid order ID.");
            if (string.IsNullOrWhiteSpace(resolution)) return BadRequest("Resolution must not be empty.");

            try
            {
                var result = await _adminService.ResolveDisputeAsync(orderId, resolution);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while resolving the dispute.");
            }
        }


        [HttpGet("getAllRestaurants")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            try
            {
                var restaurants = await _adminService.GetAllRestaurantsAsync();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving restaurant details.");
            }
        }
    }
}
