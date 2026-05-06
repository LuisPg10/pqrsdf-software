using Domain.Entities.SolicitudeResponses;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class SolicitudeResponseRepository(ApplicationDbContext db)
  : GenericRepository<SolicitudeResponse>(db), ISolicitudeResponseRepository
{
  public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
  {
    return db.SolicitudeTypes.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
  }
}