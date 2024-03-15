using Promact.CustomerSuccess.Platform.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class UpdateClientFeedbackDto
    {

        public Guid ProjectId { get; set; }
        public DateTime DateReceived { get; set; }
        public FeedbackType FeedbackType { get; set; }

        public string DetailedFeedback { get; set; }

        public bool ActionTaken { get; set; }

        public DateTime? ClosureDate { get; set; }

    }
}
