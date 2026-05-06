using Application.Utilities;
using Domain.Repositories;

namespace Application.Auth.Login;

public class LoginQueryHandler(IUserRepository userRepository, ITokenHandler tokenHandler)
  : IRequestHandler<LoginQueryDto, ErrorOr<AuthResponseDto>>
{
  public async Task<ErrorOr<AuthResponseDto>> Handle(LoginQueryDto request, CancellationToken cancellationToken)
  {
    var password = tokenHandler.EncryptText(request.Password);

    var user = await userRepository.GetUserByEmailOrPassword(request.Email, password);
    if (user == null) return Error.NotFound("User.NotFound", "Incorrect email or password");

    return new AuthResponseDto
    {
      Token = tokenHandler.GenerateJwt(user, 2)
    };
  }
}