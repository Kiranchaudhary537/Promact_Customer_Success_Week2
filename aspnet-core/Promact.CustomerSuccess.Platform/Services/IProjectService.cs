using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IProjectService : IApplicationService
    {
        Task<List<Project>> GetProjectAsync();
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<Project> CreateProjectAsync(CreateProjectDto input);
        Task<Project> UpdateProjectAsync(Guid id, UpdateProjectDto input);
        Task<String> DeleteProjectAsync(Guid id);
    }
}
