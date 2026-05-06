using Domain.Entities.SolicitudeResponses;
using Domain.Entities.Solicitudes;

namespace Application.Clients.GetPQRSDF;

public record PQRSDFResponseDto
{
  public SolicitudeStatusEnum CurrentStatus { get; init; }
  public DateTime DateSolicitude { get; set; }
  public DateTime ExpirationDate { get; set; }
  public int Days { get; set; }
  public bool HasResponse { get; set; }
  public SolicitudeResponse? Response { get; set; }
}