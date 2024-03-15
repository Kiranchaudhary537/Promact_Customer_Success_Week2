using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class VersionHistory : AuditedAggregateRootWithUser<Guid, ApplicationUser>
    {
        public string Version { get; set; }
        public string Change { get; set; }
        public string ChangeReason { get; set; }
        public string CreatedBy { get; set; }
        public DateTime RevisionDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ApprovedBy { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
