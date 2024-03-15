using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Services.Service
{
    public interface IApprovedTeamService:IApplicationService
    {
        Task<List<ApprovedTeam>> GetAllAsync();
        Task<ApprovedTeam> GetByIdAsync(Guid id);
        Task<ApprovedTeam> CreateAsync(CreateApprovedTeamDto input);
        Task UpdateAsync(Guid id, UpdateApprovedTeamDto input);
        Task DeleteAsyncById(Guid id);
    }
}
