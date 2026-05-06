using Domain.Entities.Solicitudes;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories
{
    public class SolicitudeTypeRepository(ApplicationDbContext db) : GenericRepository<SolicitudeType>(db), ISolicitudeTypeRepository
    {
        public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return db.SolicitudeTypes.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
