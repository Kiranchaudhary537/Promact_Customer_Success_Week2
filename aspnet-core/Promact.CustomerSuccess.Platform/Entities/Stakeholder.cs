using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class Stakeholder:Entity<Guid>
    {
        public StakeholderTitle Title { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
    }
}
