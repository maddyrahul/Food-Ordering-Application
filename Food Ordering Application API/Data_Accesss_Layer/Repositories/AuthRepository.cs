﻿using Data_Accesss_Layer.DTOs;
using Data_Accesss_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data_Accesss_Layer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }



        public async Task<bool> CreateUser(RegisterModelDTO registerDto)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = registerDto.PhoneNumber,
                Role = registerDto.Role
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(registerDto.Role))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(registerDto.Role));
                    if (!roleResult.Succeeded)
                    {
                        return false;
                    }
                }
                await _userManager.AddToRoleAsync(user, registerDto.Role);
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser> GetUserByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<bool> CheckPassword(LoginModelDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            return user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password);
        }

        public async Task<string> GetUserRole(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Count > 0 ? roles[0] : null;
        }
    }
}

