using Promact.CustomerSuccess.Platform.Entities;
using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateProjectBudgetDto
    {
        public ProjectType ProjectType { get; set; }
        public int? DurationInMonths { get; set; }
        public int? BudgetedHours { get; set; }
        public Guid ProjectId { get; set; }
    }
}
