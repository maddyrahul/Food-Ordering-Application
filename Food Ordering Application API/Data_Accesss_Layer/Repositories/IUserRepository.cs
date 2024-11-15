using Data_Accesss_Layer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();

        // For editing email, username, and isActive
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

        // Delete a user
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        // Check if user exists by email
        Task<bool> UserExistsAsync(string email);

        // Get user by email
        Task<ApplicationUser> GetUserByEmail(string email);


    }
}
