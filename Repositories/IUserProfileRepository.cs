using GamersDock.Entities;

namespace GamersDock.Repositories
{
    public interface IUserProfileRepository
    {

        public Task<List<Profiles>> Get();
        public Task<Profiles> Get(int id);
        public Task Create(Profiles profile);
        public Task Update(Profiles profile);
        public Task Delete(Profiles profile);

    }
}
