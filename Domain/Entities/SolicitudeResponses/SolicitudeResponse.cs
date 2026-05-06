using Domain.Generics;

namespace Domain.Entities.SolicitudeResponses;

public class SolicitudeResponse : GenericEntity
{
  [Required] public string Content { get; private set; } = string.Empty;
  [Required] public DateTime Date { get; private set; } = DateTime.UtcNow;

  // Foreign keys
  [Required] public Guid SolicitudeId { get; private set; }
  [Required] public Guid UserId { get; private set; }
}