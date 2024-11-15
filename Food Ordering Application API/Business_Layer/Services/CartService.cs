using Data_Accesss_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<object>> GetCartItemsAsync(string customerId)
        {
            if (customerId == null)
                throw new ArgumentException("Invalid customer ID.");

            try
            {
                return   await _cartRepository.GetCartItemsAsync(customerId);
               // return cartItems.Select(c => new { c.Menu.Name, c.Quantity, c.Menu.Price });
            }
            catch (Exception ex)
            {
                // Log exception if needed
                throw new Exception("An error occurred while retrieving cart items.", ex);
            }
        }

        public async Task<string> RemoveCartItemAsync(string cartId)
        {
            if (cartId == null)
                throw new ArgumentException("Invalid cart ID.");

            try
            {
                var cartItem = await _cartRepository.GetCartItemByIdAsync(cartId);
                if (cartItem == null) return "Cart item not found.";

                await _cartRepository.RemoveCartItemAsync(cartItem);
                return "Item removed from cart.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the cart item.", ex);
            }
        }

        public async Task<string> UpdateCartItemAsync(string cartId, int quantity)
        {
            if (cartId == null)
                throw new ArgumentException("Invalid cart ID.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            try
            {
                var cartItem = await _cartRepository.GetCartItemByIdAsync(cartId);
                if (cartItem == null) return "Cart item not found.";

                cartItem.Quantity = quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
                return "Cart item quantity updated.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the cart item quantity.", ex);
            }
        }
    }
}
