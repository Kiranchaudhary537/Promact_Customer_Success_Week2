    using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class Users : Entity<Guid>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

    }
}
