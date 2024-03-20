using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class UserProject:Entity<Guid>
    {
        [ForeignKey("Users")]
        public Guid UsersId { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
