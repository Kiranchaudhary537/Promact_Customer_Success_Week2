using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Promact.CustomerSuccess.Platform
{

    public class TodoAppService :ApplicationService
        
    {


        public Task<List<TodoItemDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Project> CreateAsync(CreateTodoItemDto input)
        {
            throw new NotImplementedException();
        }

        Task UpdateAsync(Guid id, UpdateTodoItemDto input)
        {
            throw new NotImplementedException();
        }
    }
}
