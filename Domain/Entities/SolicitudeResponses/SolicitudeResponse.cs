using Domain.Entities.Solicitudes;
using Domain.Entities.Users;
using Domain.Generics;

namespace Domain.Entities.SolicitudeResponses;

public class SolicitudeResponse : GenericEntity
{
  [Required] public string Content { get; private set; } = string.Empty;
  [Required] public DateTime Date { get; private set; } = DateTime.UtcNow;

  // Foreign keys
  [Required] public Guid SolicitudeId { get; private set; }
  [ForeignKey("SolicitudeId")] public Solicitude Solicitude { get; set; }

  [Required] public Guid UserId { get; private set; }
  [ForeignKey("UserId")] public User User { get; set; }

  public static SolicitudeResponse Create(string content, Guid solicitudeId, Guid userId)
  {
    return new SolicitudeResponse
    {
      Id = Guid.NewGuid(),
      Content = content,
      Date = DateTime.UtcNow,
      SolicitudeId = solicitudeId,
      UserId = userId,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };
  }
}