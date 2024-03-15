using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform
{
    public interface ITodoAppService : IApplicationService
    {
        Task<List<TodoItemDto>> GetAsync();
        Task<ProjectDto> GetByIdAsync(Guid id);
        Task<Project> CreateAsync(CreateTodoItemDto input);
        Task UpdateAsync(Guid id, UpdateTodoItemDto input);
        Task DeleteAsync(Guid id);
    }
}
