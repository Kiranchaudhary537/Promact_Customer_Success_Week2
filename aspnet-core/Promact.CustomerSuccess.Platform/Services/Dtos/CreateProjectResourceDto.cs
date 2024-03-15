namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateProjectResourceDto
    {
        public Guid ProjectId { get; set; }
        public double AllocationPercentage { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public required string Role { get; set; }

        public required string Name { get; set; }
        //public string? Comment { get; set; }
    }
}
