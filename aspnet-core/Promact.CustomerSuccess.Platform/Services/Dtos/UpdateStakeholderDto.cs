using Promact.CustomerSuccess.Platform.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class UpdateStakeholderDto
    {
        public StakeholderTitle Title { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
    }
}
