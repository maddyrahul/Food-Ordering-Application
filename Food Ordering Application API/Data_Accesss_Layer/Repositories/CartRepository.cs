using Data_Accesss_Layer.Data;
using Data_Accesss_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Accesss_Layer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetCartItemsAsync(string customerId)
        {
            return await _context.Carts
                .Include(c => c.Menu)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Cart> GetCartItemByIdAsync(string cartId)
        {
            return await _context.Carts.FindAsync(cartId);
        }

        public async Task AddCartItemAsync(Cart cartItem)
        {
            await _context.Carts.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(Cart cartItem)
        {
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(Cart cartItem)
        {
            _context.Carts.Update(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
