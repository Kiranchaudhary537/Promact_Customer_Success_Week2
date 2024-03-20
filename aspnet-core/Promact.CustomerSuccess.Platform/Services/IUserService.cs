using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services
{
    public interface IUserService : IApplicationService
    {
        Task<List<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(Guid id);
        Task<Users> CreateAsync(CreateUsersDto input);
        Task UpdateAsync(Guid id, UpdateUsersDto input);
        Task DeleteAsyncById(Guid id);
    }
}
