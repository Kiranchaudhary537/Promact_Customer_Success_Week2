using Promact.CustomerSuccess.Platform.Entities;
using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class ProjectDto: IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ProjectManager { get; set; }
        public required string Status { get; set; }
        public required string Member { get; set; }
        public IEnumerable<ProjectResourceDto>? Resources { get; set; }
        public IEnumerable<ClientFeedbackDto>? ClientFeedbacks { get; set; }
        public IEnumerable<MeetingMinuteDto>? MeetingMinutes { get; set; }
        public IEnumerable<Phase>? Phases { get; set; }
        public IEnumerable<ApprovedTeam>? ApprovedTeams { get; set; }
        public IEnumerable<ProjectUpdate>? ProjectUpdates { get; set; }
        public IEnumerable<ProjectBudget>? ProjectBudgets { get; set; }
        public IEnumerable<VersionHistory>? versionHistories { get; set; }
    }
}
