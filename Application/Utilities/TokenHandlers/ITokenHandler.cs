using Domain.Entities.Users;

namespace Application.Utilities.TokenHandlers;

public interface ITokenHandler
{
  string GenerateJwt(User user, int option);
}