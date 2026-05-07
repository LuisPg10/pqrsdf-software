namespace Application.SolicitudeTypes.CreateType;

public record CreateSolicitudeTypeResponseDto(string Message)
{
  public string Message { get; set; } = Message;
}