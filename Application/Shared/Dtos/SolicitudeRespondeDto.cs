namespace Application.Shared.Dtos
{
    public record SolicitudeRespondeDto(
        Guid Id,
        string Content,
        UserResponseDto User,
        DateTime CreatedAt
    );
}
