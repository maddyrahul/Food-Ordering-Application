using Data_Accesss_Layer.Models;
using Data_Accesss_Layer.DTOs;

namespace Data_Accesss_Layer.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> UserExists(string username);
        Task<ApplicationUser> GetUserById(string userId);
        Task<bool> CreateUser(RegisterModelDTO registerDto);
        Task<bool> CheckPassword(LoginModelDTO loginDto);
        Task<string> GetUserRole(string username);
        Task<ApplicationUser> GetUserByName(string username);
        Task<ApplicationUser> GetUserByUsername(string username);


    }
}
