using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IAuditHistoryService : IApplicationService
    {
        Task<List<AuditHistory>> GetAllAsync();
        Task<AuditHistory> GetByIdAsync(Guid id);
        Task<AuditHistory> CreateAsync(CreateAuditHistoryDto input);
        Task UpdateAsync(Guid id, UpdateAuditHistoryDto input);
        Task<string> DeleteAsyncById(Guid id);
    }
}
