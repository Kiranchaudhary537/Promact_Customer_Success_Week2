using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class AuditHistory : Entity<Guid>
    {
        public DateTime DateOfAudit { get; set; }
        public string ReviewedBy { get; set; }
        public string Status { get; set; }
        public string Section { get; set; }
        public string CommentQueries { get; set; }
        public string ActionItem { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
