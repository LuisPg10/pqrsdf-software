using Domain.Generics;

namespace Domain.Entities.Solicitudes;

public class SolicitudeType : GenericEntity
{
  [Required] public string Name { get; private set; } = string.Empty;
  [Required] public int Time { get; private set; }
}