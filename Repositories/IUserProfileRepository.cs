using GamersDock.Entities;
using GamersDock.Dtos;

namespace GamersDock.Repositories
{
    public interface IUserProfileRepository
    {
        Task<List<Profiles>> GetAllAsync();
        Task<Profiles?> GetAsync(int id);
        Task<Profiles> CreateAsync(string userId, CreateProfileRequest request);
        Task<Profiles?> UpdateAsync(int id, UpdateProfileRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAvatarAsync(int id, UpdateAvatarRequest request);
        Task<bool> IsOwnedByUserAsync(int profileId, string userId);
    }
}