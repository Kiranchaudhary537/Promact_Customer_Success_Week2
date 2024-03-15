namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateStakeAndScopeDto
    {
        public string Stake { get; set; }
        public string Scope { get; set; }

        public Guid ProjectId { get; set; }
    }
}
