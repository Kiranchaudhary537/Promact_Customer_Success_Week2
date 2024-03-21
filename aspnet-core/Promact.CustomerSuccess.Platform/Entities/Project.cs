    using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class Project : AuditedEntity<Guid>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ProjectManager { get; set; }

        public required string Status { get; set; }
        public required string Member { get; set; }

        
        public virtual ICollection<ProjectResource>? Resources { get; set; }
        public virtual ICollection<ClientFeedback>? ClientFeedbacks { get; set; }
        public virtual ICollection<MeetingMinute>? MeetingMinutes { get; set; }
        public virtual ICollection<ProjectUpdate>? ProjectUpdates { get; set; }
        public virtual ICollection<ProjectBudget>? ProjectBudgets { get; set; }
        public virtual ICollection<VersionHistory>? VersionHistories { get; set; }
        public virtual ICollection<Stakeholder>? Stakeholders { get; set; }

        public virtual ICollection<Phase>? Phases
        {
            get; set;
        }
        }
}
