using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccessPlatform.App.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class PhaseDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }
  
        public Guid ProjectId { get; set; }
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public PhaseStatus Status { get; set; }

        public string Comments { get; set; }
        public IEnumerable<ApprovedTeamDto>? ApprovedTeams { get; set; }
    }
}
