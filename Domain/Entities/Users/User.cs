using Domain.Generics;

namespace Domain.Entities.Users;

public class User : GenericEntity
{
  [Required] public string Name { get; private set; } = string.Empty;
  [Required] public string LastName { get; private set; } = string.Empty;
  [Required] public string Email { get; private set; } = string.Empty;
  [Required] public string Password { get; private set; } = string.Empty;
  [Required] public UserRoleEnum Role { get; private set; }
}