using Domain.Entities.Clients;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class ClientRepository(ApplicationDbContext db) : GenericRepository<Client>(db), IClientRepository
{
  public Task<Client?> GetByEmail(string email)
  {
    return db.Clients.FirstOrDefaultAsync(u => u.Email == email);
  }
}