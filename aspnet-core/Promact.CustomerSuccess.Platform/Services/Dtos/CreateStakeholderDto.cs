using Promact.CustomerSuccess.Platform.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateStakeholderDto
    {
        public StakeholderTitle Title { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
    }
}
