using Domain.Entities.Solicitudes;

namespace Application.Users.GetPQRSDFDetails;

public class DetailsSolicitudeDto
{
  public AreaEnum Area { get; private set; }
  public string Subject { get; private set; } = string.Empty;
  public string Description { get; private set; } = string.Empty;
  public DateTime DateSolicitude { get; private set; } = DateTime.UtcNow;
  public string FiledNumber { get; private set; } = string.Empty;
  public SolicitudeStatusEnum Status { get; private set; }
  public Guid ClientId { get; private set; }
  public Guid TypeId { get; private set; }
}