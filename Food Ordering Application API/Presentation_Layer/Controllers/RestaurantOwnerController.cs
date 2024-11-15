/*using Microsoft.AspNetCore.Mvc;
using Business_Layer.Services;
using Data_Accesss_Layer.DTOs;
 

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantOwnerController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantOwnerController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        *//* [HttpPost("registerRestaurant")]
         public IActionResult RegisterRestaurant(int ownerId, string name, string location, string cuisine)
         {
             try
             {
                 var restaurant = _restaurantService.RegisterRestaurant(ownerId, name, location, cuisine);
                 return Ok(restaurant);
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }*//*

        [HttpPost("registerRestaurant")]
        public IActionResult RegisterRestaurant([FromBody] RegisterRestaurantDto registerRestaurantDto)
        {
            try
            {
                var restaurant = _restaurantService.RegisterRestaurant(registerRestaurantDto.OwnerId, registerRestaurantDto.Name, registerRestaurantDto.Location, registerRestaurantDto.Cuisine);
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("addMenuItem")]
        public IActionResult AddMenuItem([FromBody] MenuItemDto menuItemDto)
        {
            try
            {
                var menuItem = _restaurantService.AddMenuItem(menuItemDto.RestaurantId, menuItemDto.Name, menuItemDto.Price);
                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("editMenuItem/{menuId}")]
        public IActionResult EditMenuItem(int menuId, string name, decimal price)
        {
            try
            {
                var menuItem = _restaurantService.EditMenuItem(menuId, name, price);
                return Ok(menuItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu item not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteMenuItem/{menuId}")]
        public IActionResult DeleteMenuItem(int menuId)
        {
            try
            {
                _restaurantService.DeleteMenuItem(menuId);
                return Ok("Menu item deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu item not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("viewReviews/{restaurantId}")]
        public IActionResult ViewReviews(int restaurantId)
        {
            var reviews = _restaurantService.ViewReviews(restaurantId);
            return Ok(reviews);
        }

        [HttpPost("respondToReview")]
        public IActionResult RespondToReview(int reviewId, string response)
        {
            try
            {
                _restaurantService.RespondToReview(reviewId, response);
                return Ok("Response added to review.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Review not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
*/

using Microsoft.AspNetCore.Mvc;
using Business_Layer.Services;
using Data_Accesss_Layer.DTOs;


namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantOwnerController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantOwnerController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("registerRestaurant")]
        public IActionResult RegisterRestaurant([FromBody] RegisterRestaurantDto registerRestaurantDto)
        {
            try
            {
                var restaurant = _restaurantService.RegisterRestaurant(registerRestaurantDto.OwnerId, registerRestaurantDto.Name, registerRestaurantDto.Location, registerRestaurantDto.Cuisine);
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("addMenuItem")]
        public IActionResult AddMenuItem([FromBody] MenuItemDto menuItemDto)
        {
            try
            {
                var menuItem = _restaurantService.AddMenuItem(menuItemDto.RestaurantId, menuItemDto.Name, menuItemDto.Price);
                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("menuItem/{menuId}")]
        public IActionResult GetMenuItemById(string menuId)
        {
            var menuItem = _restaurantService.GetMenuItemById(menuId);
            if (menuItem == null)
            {
                return NotFound("Menu item not found.");
            }
            return Ok(menuItem);
        }

        [HttpPut("editMenuItem/{menuId}")]
        public IActionResult EditMenuItem(string menuId, [FromBody] MenuItemDto menuItemDto)
        {
            try
            {
                var menuItem = _restaurantService.EditMenuItem(menuId, menuItemDto.Name, menuItemDto.Price);
                return Ok(menuItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu item not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteMenuItem/{menuId}")]
        public IActionResult DeleteMenuItem(string menuId)
        {
            try
            {
                _restaurantService.DeleteMenuItem(menuId);
                return Ok(new { message = "Menu deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu item not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("viewReviews/{restaurantId}")]
        public IActionResult ViewReviews(string restaurantId)
        {
            var reviews = _restaurantService.ViewReviews(restaurantId);
            return Ok(reviews);
        }

        [HttpPost("respondToReview")]
        public IActionResult RespondToReview(string reviewId, string response)
        {
            try
            {
                _restaurantService.RespondToReview(reviewId, response);
                return Ok("Response added to review.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Review not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
