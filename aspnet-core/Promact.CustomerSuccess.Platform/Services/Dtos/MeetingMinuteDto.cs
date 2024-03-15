using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class MeetingMinuteDto: IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        public DateTime MeetingDate { get; set; }
        public string MoMLink { get; set; }

        public string Comments { get; set; }    

        public int Duration { get; set; }
    }
}