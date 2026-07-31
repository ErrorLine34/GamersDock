using System.Security.Claims;
using GamersDock.Entities;
using GamersDock.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamersDock.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<Users> _userManager;

        public UserRepository(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _userManager.Users
                .ToListAsync();
        }

        public async Task<Users?> GetCurrentAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<bool> UpdateCurrentAsync(ClaimsPrincipal user, UpdateUserRequest request)
        {
            var current = await _userManager.GetUserAsync(user);
            if (current is null) return false;

            if (request.UserName is not null)
            {
                var result = await _userManager.SetUserNameAsync(current, request.UserName);
                if (!result.Succeeded) return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId.ToString());
            if (userToDelete is null) return false;

            var result = await _userManager.DeleteAsync(userToDelete);
            return result.Succeeded;
        }
    }
}
