namespace Application.Clients.CreatePQRSDF;

public class ClientCommandDto
{
  [Required] public string Name { get; private set; } = string.Empty;
  [Required] public string LastName { get; private set; } = string.Empty;
  [Required] public string Email { get; private set; } = string.Empty;
}