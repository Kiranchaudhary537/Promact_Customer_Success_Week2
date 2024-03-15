using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IOverviewService : IApplicationService
    {
        Task<List<Overview>> GetAllAsync();
        Task<Overview> GetByIdAsync(Guid id);
        Task<Overview> CreateAsync(CreateOverviewDto input);
        Task<Overview> UpdateAsync(Guid id, UpdateOverviewDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
