using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Service
{
    public interface IPhaseService : IApplicationService
    {
        Task<List<Phase>> GetAllAsync();
        Task<Phase> GetByIdAsync(Guid id);
        Task<Phase> CreateAsync(CreatePhaseDto input);
        Task UpdateAsync(Guid id, UpdatePhaseDto input);
        Task DeleteAsync(Guid id);
    }
}
