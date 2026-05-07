namespace Application.Clients.CreatePQRSDF;

public record SolicitudeResponseDto
{
  public string FiledNumber { get; init; } = string.Empty;
  public DateTime DueDate { get; init; }
  public string Message { get; init; } = string.Empty;
}