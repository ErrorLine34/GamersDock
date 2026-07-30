using System.Security.Claims;
using GamersDock.Entities;
using GamersDock.Dtos;

namespace GamersDock.Repositories
{
    public interface IUserRepository
    {
        Task<Users?> GetCurrentAsync(ClaimsPrincipal user);
        Task<bool> UpdateCurrentAsync(ClaimsPrincipal user, UpdateUserRequest request);
    }
}
