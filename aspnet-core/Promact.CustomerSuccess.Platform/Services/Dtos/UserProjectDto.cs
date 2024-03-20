using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class UserProjectDto: IEntityDto<Guid>
    {

        public Guid Id { get; set; }
        [ForeignKey("Users")]
        public Guid UsersId { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
