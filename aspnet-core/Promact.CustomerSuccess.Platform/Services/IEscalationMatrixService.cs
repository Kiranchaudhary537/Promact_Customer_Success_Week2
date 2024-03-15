using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IEscalationMatrixService : IApplicationService
    {
        Task<List<EscalationMatrix>> GetAllAsync();
        Task<EscalationMatrix> GetByIdAsync(Guid id);
        Task<EscalationMatrix> CreateAsync(CreateEscalationMatrixDto input);
        Task UpdateAsync(Guid id, UpdateEscalationMatrixDto input);
        Task<string> DeleteAsyncById(Guid id);
    }
}
