namespace Application.Clients.CreatePQRSDF;

public class ClientCommandDto
{
  [Required] public string Name { get; init; } = string.Empty;
  [Required] public string LastName { get; init; } = string.Empty;
  [Required] public string Email { get; init; } = string.Empty;
}