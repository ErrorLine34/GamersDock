using GamersDock.Entities;
using GamersDock.Data;
using GamersDock.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GamersDock.Repositories
{
    public class ProfilesRepository : IUserProfileRepository
    {
        private readonly GamersDockContext _context;

        public ProfilesRepository(GamersDockContext context)
        {
            _context = context;
        }

        public async Task<List<Profiles>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profiles?> GetAsync(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<Profiles> CreateAsync(CreateProfileRequest request)
        {
            var profile = new Profiles { ProfileName = request.Name, Avatar = request.Avatar, CreatedAt = DateTime.UtcNow };
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<Profiles?> UpdateAsync(int id, UpdateProfileRequest request)
        {
            var existing = await _context.Profiles.FindAsync(id);
            if (existing is null) return null;
            existing.ProfileName = request.Name;
            _context.Profiles.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Profiles.FindAsync(id);
            if (existing is null) return false;
            _context.Profiles.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAvatarAsync(int id, UpdateAvatarRequest request)
        {
            var existing = await _context.Profiles.FindAsync(id);
            if (existing is null) return false;
            existing.Avatar = request.Avatar;
            _context.Profiles.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
