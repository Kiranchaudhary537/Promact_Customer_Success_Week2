using Promact.CustomerSuccess.Platform.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class UpdateRemediationStepDto
    {
        public required string Description { get; set; }
        public Guid RiskProfileId { get; set; }
        public virtual RiskProfile? RiskProfile { get; set; }
    }
}
