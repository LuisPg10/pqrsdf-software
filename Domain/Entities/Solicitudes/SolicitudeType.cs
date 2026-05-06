namespace Domain.Entities.Solicitudes;

public class SolicitudeType
{
  [Required] public Guid Id { get; private set; } = Guid.NewGuid();
  [Required] public string Name { get; private set; } = string.Empty;
  [Required] public int Time { get; private set; }
}