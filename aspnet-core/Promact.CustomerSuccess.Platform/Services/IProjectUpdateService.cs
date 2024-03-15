using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Service
{
    public interface IProjectUpdateService : IApplicationService
    {
        Task<List<ProjectUpdate>> GetAllAsync();
        Task<ProjectUpdate> GetByIdAsync(Guid id);
        Task<ProjectUpdate> CreateAsync(CreateProjectUpdateDto input);
        Task<ProjectUpdate> UpdateAsync(Guid id, UpdateProjectUpdateDto input);
        Task<String> DeleteAsync(Guid id);
    }
}
