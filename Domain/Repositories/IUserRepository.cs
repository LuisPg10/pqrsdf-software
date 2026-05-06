using Domain.Entities.Users;
using Domain.Shared.Repositories;

namespace Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
  Task<User?> GetUserByEmail(string email);
  Task<bool> UserExists(string email);
}