
using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform
{
    public interface IClientFeedbackService : IApplicationService
    {
        Task<List<ClientFeedback>> GetAllAsync();
        Task<ClientFeedback> GetByIdAsync(Guid id);
        Task<ClientFeedback> CreateAsync(CreateClientFeedbackDto input);
        Task<ClientFeedback> UpdateAsync(Guid id, UpdateClientFeedbackDto input);
        Task<String> DeleteAsync(Guid id);
    }
}
