using GamersDock.Entities;
using GamersDock.Dtos;

namespace GamersDock.Repositories
{
    public interface IInstanceSettingsRepository
    {
        Task<InstanceSettings> GetAsync();
        Task<InstanceSettings> UpdateAsync(UpdateInstanceSettingsRequest request);
    }
}
