namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateOverviewDto
    {
        public string Brief { get; set; }
        public string Purpose { get; set; }
        public string Goals { get; set; }
        public string Objectives { get; set; }
        public string Budget { get; set; }
        public Guid ProjectId { get; set; }
    }
}
