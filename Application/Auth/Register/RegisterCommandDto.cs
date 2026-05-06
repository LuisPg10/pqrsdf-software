namespace Application.Auth.Register;

public class RegisterCommandDto : IRequest<ErrorOr<AuthResponseDto>>
{
  [Required(ErrorMessage = "name is required.")]
  public string Name { get; set; } = string.Empty;

  [Required(ErrorMessage = "lastName is required.")]
  public string LastName { get; set; } = string.Empty;

  [Required(ErrorMessage = "email is required.")]
  [EmailAddress(ErrorMessage = "Invalid email address.")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "password is required.")]
  [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
    ErrorMessage = "The password must have uppercase, lowercase, number and special character.")
  ]
  public string Password { get; set; } = string.Empty;
}