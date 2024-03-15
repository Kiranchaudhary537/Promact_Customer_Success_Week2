using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class StakeAndScopeDto:IEntityDto<Guid>
    {
        public Guid Id { get; set; }
 
            public string Stake { get; set; }
            public string Scope { get; set; }
       
            public Guid ProjectId { get; set; }
    }
}
