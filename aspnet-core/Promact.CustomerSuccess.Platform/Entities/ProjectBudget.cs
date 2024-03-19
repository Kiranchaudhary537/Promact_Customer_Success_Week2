using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Promact.CustomerSuccess.Platform.Entities
{ 
    public class ProjectBudget : Entity<Guid>
    {
        public ProjectType Type { get; set; }
        public int? DurationInMonths { get; set; }
        public int? BudgetedHours { get; set; }
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public override object?[] GetKeys()
        {
            //Implement IReadOnlyEntityType.GetKeys Method to return the keys of the entity.
            return new object[] { nameof(Type) };

        }
    }
}