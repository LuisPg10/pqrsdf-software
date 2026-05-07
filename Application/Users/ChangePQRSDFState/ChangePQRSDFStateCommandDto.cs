using Domain.Entities.Solicitudes;

namespace Application.Users.ChangePQRSDFState;

public record ChangePQRSDFStateCommandDto : IRequest<ErrorOr<Unit>>
{
  [Required] public Guid Id { get; set; }
  [Required] public SolicitudeStatusEnum NewStatus { get; set; }
  [Required] public string Justification { get; init; } = "";
}