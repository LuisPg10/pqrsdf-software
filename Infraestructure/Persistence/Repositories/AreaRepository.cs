using Domain.Entities.Areas;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories
{
    public class AreaRepository(ApplicationDbContext db) : GenericRepository<Area>(db), IAreaRepository
    {
        public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return db.SolicitudeTypes.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
