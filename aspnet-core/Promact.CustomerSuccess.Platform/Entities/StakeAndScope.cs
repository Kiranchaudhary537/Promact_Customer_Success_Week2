using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class StakeAndScope:Entity<Guid>
    {
        public string Stake { get; set; }
        public string Scope { get; set; }
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
