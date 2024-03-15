using Promact.CustomerSuccess.Platform.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class RemediationStepDto:IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public Guid RiskProfileId { get; set; }
        public virtual RiskProfile? RiskProfile { get; set; }
    }
}
