using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Service
{
    public interface IProjectResourceService : IApplicationService
    {
        Task<List<ProjectResource>> GetAllAsync();
        Task<ProjectResource> GetByIdAsync(Guid id);
        Task<ProjectResource> CreateAsync(CreateProjectResourceDto input);
        Task<ProjectResource> UpdateAsync(Guid id, UpdateProjectResourceDto input);
        Task<String> DeleteAsync(Guid id);
    }
}
