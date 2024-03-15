using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class OverviewDto:IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public string Brief { get; set; }
        public string Purpose { get; set; }
        public string Goals { get; set; }
        public string Objectives { get; set; }
        public string Budget { get; set; }
        public Guid ProjectId { get; set; }
    }
}
