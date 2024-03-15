using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccessPlatform.App.Entities;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class UpdatePhaseDto
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }   

        public DateTime? ApprovalDate { get; set; }

        public PhaseStatus Status { get; set; }

        public string Comments { get; set; }
    }
}
