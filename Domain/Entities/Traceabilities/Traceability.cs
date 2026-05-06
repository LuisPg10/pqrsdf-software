using Domain.Entities.Solicitudes;

namespace Domain.Entities.Traceabilities;

public class Traceability
{
  [Required] public Guid Id { get; private set; } = Guid.NewGuid();
  [Required] public string Action { get; private set; } = string.Empty;
  [Required] public string Description { get; private set; } = string.Empty;
  [Required] public SolicitudeStatusEnum LastStatus { get; private set; }
  [Required] public SolicitudeStatusEnum NewStatus { get; private set; }
  [Required] public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

  // Foreign keys
  [Required] public Guid SolicitudeId { get; private set; }
}