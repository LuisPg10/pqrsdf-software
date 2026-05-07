namespace Application.Shared.Dtos
{
    public record UserResponseDto(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string Role
    );
}
