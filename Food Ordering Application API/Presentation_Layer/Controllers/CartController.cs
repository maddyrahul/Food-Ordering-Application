/*using Business_Layer.Services;
using Data_Accesss_Layer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get cart items for a customer
        [HttpGet("getCartItems/{customerId}")]
        public async Task<IActionResult> GetCartItems(int customerId)
        {
            if (customerId <= 0) return BadRequest("Invalid customer ID.");

            try
            {
                var cartItems = await _cartService.GetCartItemsAsync(customerId);
                return Ok(cartItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(500, "An error occurred while retrieving cart items.");
            }
        }

        // Remove item from cart
        [HttpDelete("removeCartItem/{cartId}")]
        public async Task<IActionResult> RemoveCartItem(int cartId)
        {
            if (cartId <= 0) return BadRequest("Invalid cart ID.");

            try
            {
                var result = await _cartService.RemoveCartItemAsync(cartId);
                if (result == "Cart item not found.") return NotFound(result);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while removing the cart item.");
            }
        }

        // Update item quantity in cart
        [HttpPut("updateCartItem")]
        public async Task<IActionResult> UpdateCartItem(int cartId, int quantity)
        {
            if (cartId <= 0) return BadRequest("Invalid cart ID.");
            if (quantity <= 0) return BadRequest("Quantity must be greater than zero.");

            try
            {
                var result = await _cartService.UpdateCartItemAsync(cartId, quantity);
                if (result == "Cart item not found.") return NotFound(result);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the cart item quantity.");
            }
        }
    }
}
*/

using Business_Layer.Services;
using Data_Accesss_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        // Get cart items for a customer
        [Authorize(Roles = "Customer")]
        [HttpGet("getCartItems/{customerId}")]
        public async Task<IActionResult> GetCartItems(string customerId)
        {
            if (customerId == null) return BadRequest("Invalid customer ID.");

            try
            {
                var cartItems = await _cartService.GetCartItemsAsync(customerId);
                return Ok(cartItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving cart items.");
            }
        }

        // Remove item from cart
        [Authorize(Roles = "Customer")]
        [HttpDelete("removeCartItem/{cartId}")]
        public async Task<IActionResult> RemoveCartItem(string cartId)
        {
            if (cartId == null) return BadRequest("Invalid cart ID.");

            try
            {
                var result = await _cartService.RemoveCartItemAsync(cartId);
                return Ok(new { message = "Item removed from cart successfully!" }); // return JSON
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while removing the cart item.");
            }
        }

        // Update item quantity in cart
        [Authorize(Roles = "Customer")]
        [HttpPut("updateCartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemDto updateCartItemDto)
        {
            if (updateCartItemDto.CartId == null) return BadRequest("Invalid cart ID.");
            if (updateCartItemDto.Quantity <= 0) return BadRequest("Quantity must be greater than zero.");

            try
            {
                var result = await _cartService.UpdateCartItemAsync(updateCartItemDto.CartId, updateCartItemDto.Quantity);


                return Ok(new { message = "Updated cart successfully!" }); // return JSON
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the cart item quantity.");
            }
        }

        
    }
}
