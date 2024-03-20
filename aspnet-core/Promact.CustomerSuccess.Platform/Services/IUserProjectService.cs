using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IUserProjectService : IApplicationService
    {
        Task<List<UserProject>> GetAllAsync();
        Task<UserProject> GetByIdAsync(Guid id);
        Task<UserProject> CreateAsync(CreateUserProjectDto input);
        Task UpdateAsync(Guid id, UpdateUserProjectDto input);
        Task DeleteAsyncById(Guid id);
}
}
