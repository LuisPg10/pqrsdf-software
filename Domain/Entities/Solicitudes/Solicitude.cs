using Domain.Entities.Clients;
using Domain.Generics;

namespace Domain.Entities.Solicitudes;

public class Solicitude : GenericEntity
{
  [Required] public AreaEnum Area { get; private set; }
  [Required] public string Subject { get; private set; } = string.Empty;
  [Required] public string Description { get; private set; } = string.Empty;
  [Required] public DateTime DateSolicitude { get; private set; } = DateTime.UtcNow;
  [Required] public string FiledNumber { get; private set; } = string.Empty;
  [Required] public SolicitudeStatusEnum Status { get; private set; }

  // Foreign keys
  [Required] public Guid ClientId { get; private set; }
  [Required] public Guid TypeId { get; private set; }
  public Guid? UserId { get; private set; }

  [ForeignKey("ClientId")] public required Client Client { get; set; }
}