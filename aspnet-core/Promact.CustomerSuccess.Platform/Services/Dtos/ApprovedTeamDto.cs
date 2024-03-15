using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class ApprovedTeamDto:IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public int NoOfResources { get; set; }
        public string Role { get; set; }
        public int Availablity { get; set; }
        public int Duration { get; set; }
        public Guid PhaseId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
