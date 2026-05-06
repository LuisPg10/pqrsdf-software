using Domain.Entities.Users;

namespace Application.Utilities;

public interface ITokenHandler
{
  string GenerateJwt(User user, int option);
}