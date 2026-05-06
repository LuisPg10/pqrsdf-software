namespace Application.Users.MakePQRSDFResponse;

public record MakePQRSDFCommandDto : IRequest<ErrorOr<SolicitudeCreatedResponseDto>>
{
  [Required] public Guid Id { get; set; }
  [Required] public string Content { get; set; } = string.Empty;
}