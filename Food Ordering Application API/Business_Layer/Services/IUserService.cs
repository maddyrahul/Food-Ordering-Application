using Data_Accesss_Layer.Models;
using Microsoft.AspNetCore.Identity;
namespace Business_Layer.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();

        // Update user details
        Task<IdentityResult> UpdateUserAsync(string userId, string newEmail, string newUsername, bool isActive);

        // Delete a user
        Task<IdentityResult> DeleteUserAsync(string userId);

        // Check if user exists by email
        Task<bool> UserExistsAsync(string email);

        // Get user by email
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
