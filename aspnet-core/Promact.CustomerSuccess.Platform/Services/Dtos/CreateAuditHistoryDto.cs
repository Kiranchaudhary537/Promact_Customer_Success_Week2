namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateAuditHistoryDto
    {
        public DateTime DateOfAudit { get; set; }
        public string ReviewedBy { get; set; }
        public string Status { get; set; }
        public string Section { get; set; }
        public string CommentQueries { get; set; }
        public string ActionItem { get; set; }
        public Guid ProjectId { get; set; }
    }
}
