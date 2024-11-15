using Data_Accesss_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartItemsAsync(string customerId);
        Task<Cart> GetCartItemByIdAsync(string cartId);
        Task AddCartItemAsync(Cart cartItem);
        Task RemoveCartItemAsync(Cart cartItem);
        Task UpdateCartItemAsync(Cart cartItem);

    }
}
