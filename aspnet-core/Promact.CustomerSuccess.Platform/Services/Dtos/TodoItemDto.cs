using Volo.Abp.Application.Dtos;

namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class TodoItemDto: IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
