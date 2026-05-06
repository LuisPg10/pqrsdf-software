using Domain.Entities.Users;
using Domain.Shared.Repositories;

namespace Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
  User? GetUserByEmailOrPassword(string email, string password);
}