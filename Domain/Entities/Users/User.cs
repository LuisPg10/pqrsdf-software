namespace Domain.Entities.Users;

public class User
{
  [Required] public Guid Id { get; private set; }
  [Required] public string Name { get; private set; } = string.Empty;
  [Required] public string LastName { get; private set; } = string.Empty;
  [Required] public string Email { get; private set; } = string.Empty;
  [Required] public UserRoleEnum Role { get; private set; }
}