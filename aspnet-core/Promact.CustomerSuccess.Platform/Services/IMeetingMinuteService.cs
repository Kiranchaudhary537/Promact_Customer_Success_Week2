using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Promact.CustomerSuccess.Platform.Service
{
    public interface IMeetingMinuteService : IApplicationService
    {
        Task<List<MeetingMinute>> GetAllAsync();
        Task<MeetingMinute> GetByIdAsync(Guid id);
        Task<MeetingMinute> CreateAsync(CreateMeetingMinuteDto input);
        Task<MeetingMinute> UpdateAsync(Guid id, UpdateMeetingMinuteDto input);
        Task<string> DeleteAsync(Guid id);
    }
}
