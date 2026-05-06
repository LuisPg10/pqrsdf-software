namespace Domain.Primitives;

public abstract class AggregateRoot
{
  private readonly List<DomainEvent> _domainEvents = [];
  private string? _currentUser;
  private string? _currentJustification;

  public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

  protected void Raise(DomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  public void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }

  public void SetOperationContext(string user, string? justification = null)
  {
    _currentUser = user;
    _currentJustification = justification;
  }

  protected string GetCurrentUser() => _currentUser ?? "system";
  protected string? GetCurrentJustification() => _currentJustification;
}