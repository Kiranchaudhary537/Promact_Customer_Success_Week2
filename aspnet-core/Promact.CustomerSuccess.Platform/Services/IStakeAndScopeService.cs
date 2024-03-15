using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IStakeAndScopeService : IApplicationService
    {
        Task<List<StakeAndScope>> GetAllAsync();
        Task<StakeAndScope> GetByIdAsync(Guid id);
        Task<StakeAndScope> CreateAsync(CreateStakeAndScopeDto input);
        Task<StakeAndScope> UpdateAsync(Guid id, UpdateStakeAndScopeDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
