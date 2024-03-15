using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class ApprovedTeam : Entity<Guid>
    {
        
        public int NumberOfResources { get; set; }
        public required string Role { get; set; }
        public int AvailabilityPercentage { get; set; }
        public int Duration { get; set; }
        [ForeignKey("Phase")]
        public Guid PhaseId { get; set; }
        
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
