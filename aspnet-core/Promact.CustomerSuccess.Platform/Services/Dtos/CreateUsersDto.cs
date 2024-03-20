namespace Promact.CustomerSuccess.Platform.Services.Dtos
{
    public class CreateUsersDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}
