 
using Data_Accesss_Layer.Models;
using Data_Accesss_Layer.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Business_Layer.Services
{
    public class UserService : IUserService
    {


        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Update email, username, and isActive only
        public async Task<IdentityResult> UpdateUserAsync(string userId, string newEmail, string newUsername, bool isActive)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Email = newEmail;
                user.UserName = newUsername;
                user.IsActive = isActive;

                return await _userRepository.UpdateUserAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        // Delete a user
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                return await _userRepository.DeleteUserAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userRepository.UserExistsAsync(email);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }


    }
}