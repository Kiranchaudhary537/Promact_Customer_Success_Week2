using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IVersionHistoryService : IApplicationService
    {
        Task<List<VersionHistory>> GetAllAsync();
        Task<VersionHistory> GetByIdAsync(Guid id);
        Task<VersionHistory> CreateAsync(CreateVersionHistoryDto input);
        Task UpdateAsync(Guid id, UpdateVersionHistoryDto input);
        Task<string> DeleteAsyncById(Guid id);
    }
}
