using Application.Utilities;
using Domain.Repositories;

namespace Application.Auth.Login;

public class LoginQueryHandler(IUserRepository userRepository, ITokenHandler tokenHandler)
  : IRequestHandler<LoginQueryDto, ErrorOr<AuthResponseDto>>
{
  public async Task<ErrorOr<AuthResponseDto>> Handle(LoginQueryDto request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetUserByEmail(request.Email);
    if (user == null) return Error.NotFound("User.NotFound", "Incorrect email or password");

    var isPasswordValid = HashPassword.Verify(request.Password, user.Password);
    if (!isPasswordValid) return Error.Validation("User.InvalidPassword", "Incorrect email or password");

    return new AuthResponseDto
    {
      Token = tokenHandler.GenerateJwt(user, 2)
    };
  }
}