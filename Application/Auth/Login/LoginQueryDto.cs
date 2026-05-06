namespace Application.Auth.Login;

public class LoginQueryDto : IRequest<ErrorOr<AuthResponseDto>>
{
  [Required(ErrorMessage = "email is required.")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "password is required.")]
  public string Password { get; set; } = string.Empty;
}