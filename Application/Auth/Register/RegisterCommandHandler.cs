using Application.Utilities;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Auth.Register;

public class RegisterCommandHandler(IUserRepository userRepository, ITokenHandler tokenHandler, IUnitOfWork unitOfWork)
  : IRequestHandler<RegisterCommandDto, ErrorOr<AuthResponseDto>>
{
  public async Task<ErrorOr<AuthResponseDto>> Handle(RegisterCommandDto request, CancellationToken cancellationToken)
  {
    var alreadyExist = await userRepository.UserExists(request.Email);
    if (alreadyExist) return Error.Validation("User.AlreadyExists", "User already exists");

    request.Password = HashPassword.Hash(request.Password);

    var user = request.Adapt<User>();
    userRepository.Create(user);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return new AuthResponseDto
    {
      Token = tokenHandler.GenerateJwt(user, 2)
    };
  }
}