using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class Sprint : Entity<Guid>
    {
        [ForeignKey("Phase")]
        public Guid PhaseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintStatus Status { get; set; }
        public required string Comments { get; set; }
        public required string Goals { get; set; }
        public int SprintNumber { get; set; }
        public virtual Phase? Phases { get; set; }
        public override object?[] GetKeys()
        {
            throw new NotImplementedException();
        }
    }
}
