using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IRiskProfileService : IApplicationService
    {
        Task<List<RiskProfile>> GetAllAsync();
        Task<RiskProfile> GetByIdAsync(Guid id);
        Task<RiskProfile> CreateAsync(CreateRiskProfileDto input);
        Task<RiskProfile> UpdateAsync(Guid id, UpdateRiskProfileDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
