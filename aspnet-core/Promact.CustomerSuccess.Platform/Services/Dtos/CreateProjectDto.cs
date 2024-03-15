using System.ComponentModel.DataAnnotations;

namespace Promact.CustomerSuccess.Platform
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(128)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ProjectManager { get; set; }
        public required string Status { get; set; }
        public required string Member { get; set; }
    }
}