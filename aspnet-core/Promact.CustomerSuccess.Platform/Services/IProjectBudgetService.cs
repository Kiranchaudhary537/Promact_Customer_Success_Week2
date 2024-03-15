using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IProjectBudgetService : IApplicationService
    {
        Task<List<ProjectBudget>> GetAllAsync();
        Task<ProjectBudget> GetByIdAsync(Guid id);
        Task<ProjectBudget> CreateAsync(CreateProjectBudgetDto input);
        Task<ProjectBudget> UpdateAsync(Guid id, UpdateProjectBudgetDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
