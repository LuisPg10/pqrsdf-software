using Domain.Entities.Solicitudes;

namespace Application.Clients.CreatePQRSDF;

public record CreatePQRSDFCommandDto : IRequest<Unit>
{
  [Required(ErrorMessage = "Area is required")]
  public AreaEnum Area { get; init; }

  [Required(ErrorMessage = "Subject is required")]
  public string Subject { get; init; } = string.Empty;

  [Required(ErrorMessage = "Description is required")]
  public string Description { get; init; } = string.Empty;

  [Required(ErrorMessage = "TypeId is required")]
  public Guid TypeId { get; init; }

  [Required(ErrorMessage = "Client information is required")]
  public ClientCommandDto Client { get; init; } = new();
}