using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface ICartService
    {

        Task<IEnumerable<object>> GetCartItemsAsync(string customerId);
        Task<string> RemoveCartItemAsync(string cartId);
        Task<string> UpdateCartItemAsync(string cartId, int quantity);
    }
}
