using Domain.Entities.Users;

namespace Application.Utilities;

public interface ITokenHandler
{
  string EncryptText(string text);
  string GenerateJwt(User user, int option);
}