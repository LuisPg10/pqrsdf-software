using Domain.Generics;

namespace Domain.Entities.Clients;

public class Client : GenericEntity
{
  [Required] public string Name { get; private set; } = string.Empty;
  [Required] public string LastName { get; private set; } = string.Empty;
  [Required] public string Email { get; private set; } = string.Empty;
}