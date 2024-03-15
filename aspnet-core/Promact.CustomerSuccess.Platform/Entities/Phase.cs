using Promact.CustomerSuccessPlatform.App.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class Phase : Entity<Guid>
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public PhaseStatus Status { get; set; }

        public string Comments { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public ICollection<ApprovedTeam>? ApprovedTeams { get; set; }
        public required string Description { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<Sprint>? Sprints { get; set; }

        public override object?[] GetKeys()
        {
            throw new NotImplementedException();
        }
    }

}
