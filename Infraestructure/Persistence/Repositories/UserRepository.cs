using Domain.Entities.Solicitudes;
using Domain.Entities.Users;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext db) : GenericRepository<User>(db), IUserRepository
{
  public Task<User?> GetUserByEmailOrPassword(string email, string password)
  {
    return db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
  }
}