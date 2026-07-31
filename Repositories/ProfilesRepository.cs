using GamersDock.Entities;
using GamersDock.Data;
using GamersDock.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamersDock.Repositories
{
    public class ProfilesRepository : IUserProfileRepository
    {
        private const string AdminRoleName = "Admin";

        private readonly GamersDockContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfilesRepository(GamersDockContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<Profiles>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profiles?> GetAsync(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }



        // CAUTION:
        // This creates a race condition, where two users might
        // create their first profile at the same time, and both get the Admin role.
        // could fix this but as this is not within scope ignore for now.
        // SORRY PHAT NO ANGY
        public async Task<Profiles> CreateAsync(string userId, CreateProfileRequest request)
        {
            // If there are no users in the system at all, this is the first account.
            var isFirstAccount = !await _context.Users.AnyAsync();

            // Is this the user's first profile
            var isFirstProfileForUser = !await _context.Profiles.AnyAsync(p => p.UserId == userId);

            var profile = new Profiles
            {
                ProfileName = request.Name,
                Avatar = request.Avatar,
                UserId = userId,
                RoleId = isFirstProfileForUser ? (int)UserRoles.Admin : (int)UserRoles.User,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            // Grant global Admin role only when this is the first account AND the first profile for that account
            if (isFirstAccount && isFirstProfileForUser)
            {
                await GrantAdminRoleAsync(userId);
            }

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

        public async Task<bool> IsOwnedByUserAsync(int profileId, string userId)
        {
            return await _context.Profiles.AnyAsync(p => p.ProfileId == profileId && p.UserId == userId);
        }

        private async Task GrantAdminRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return;

            if (!await _roleManager.RoleExistsAsync(AdminRoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(AdminRoleName));
            }

            if (!await _userManager.IsInRoleAsync(user, AdminRoleName))
            {
                await _userManager.AddToRoleAsync(user, AdminRoleName);
            }
        }
    }
}