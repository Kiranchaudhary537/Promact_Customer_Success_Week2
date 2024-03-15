using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IStakeholderService:IApplicationService
    {
        Task<List<Stakeholder>> GetAllAsync();
        Task<Stakeholder> GetByIdAsync(Guid id);
        Task<Stakeholder> CreateAsync(CreateStakeholderDto input);
        Task<Stakeholder> UpdateAsync(Guid id, UpdateStakeholderDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
