using Domain.Entities.Users;
using Domain.Shared.Repositories;

namespace Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
  Task<User?> GetUserByEmailOrPassword(string email, string password);
}