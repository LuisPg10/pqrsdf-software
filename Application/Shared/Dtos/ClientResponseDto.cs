namespace Application.Shared.Dtos
{
    public record ClientResponseDto(
        Guid Id,
        string Name,
        string LastName,
        string Email
    );
}
