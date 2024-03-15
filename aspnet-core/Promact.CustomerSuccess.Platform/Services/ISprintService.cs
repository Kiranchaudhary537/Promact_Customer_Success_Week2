using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface ISprintService : IApplicationService
    {
        Task<List<Sprint>> GetAllAsync();
        Task<Sprint> GetByIdAsync(Guid id);
        Task<Sprint> CreateAsync(CreateSprintDto input);
        Task<Sprint> UpdateAsync(Guid id, UpdateSprintDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
