using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class Overview : Entity<Guid>
    {
        public string Brief { get; set; }
        public string Purpose { get; set; }
        public string Goals { get; set; }
        public string Objectives { get; set; }
        public string Budget { get; set; }
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

    }
}
