namespace GamersDock.Dtos
{
    public record UserDto(string Id, string? UserName);

    public record UpdateUserRequest(string? UserName);
}