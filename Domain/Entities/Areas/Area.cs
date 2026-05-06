using Domain.Generics;

namespace Domain.Entities.Areas
{
  public sealed class Area : GenericEntity
  {
    [Required] public string Name { get; private set; } = string.Empty;
  }
}