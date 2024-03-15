using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace Promact.CustomerSuccess.Platform.Entities
{
    public class ClientFeedback : Entity<Guid>
    {

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public DateTime DateReceived { get; set; }
        public FeedbackType FeedbackType { get; set; }
        public required string DetailedFeedback { get; set; }
        public bool ActionTaken { get; set; }
        public DateTime? ClosureDate { get; set; }
 
        public Project? Project { get; set; }

    }
}