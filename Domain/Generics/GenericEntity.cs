using Domain.Primitives;

namespace Domain.Generics;

public abstract class GenericEntity : AggregateRoot
{
  [Required] public Guid Id { get; protected set; } = Guid.Empty;
  public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
}