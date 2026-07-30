using GamersDock.Entities;
using GamersDock.Data;

namespace GamersDock.Repositories
{
    public class ProfilesRepository : IUserProfileRepository
    {
        private readonly GamersDockContext _context;

        public ProfilesRepository(GamersDockContext context)
        {
            _context = context;
        }

        public async Task Create(Profiles profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public Task Delete(Profiles profile)
        {
            throw new NotImplementedException();
        }

        public Task<List<Profiles>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Profiles> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Profiles profile)
        {
            throw new NotImplementedException();
        }
    }
}
