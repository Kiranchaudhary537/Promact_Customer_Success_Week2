using System.ComponentModel.DataAnnotations.Schema;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class UpdateUserProjectDto
    {
        [ForeignKey("Users")]
        public Guid UsersId { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
    }
}
