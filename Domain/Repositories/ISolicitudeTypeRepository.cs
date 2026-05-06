using Domain.Entities.Solicitudes;
using Domain.Shared.Repositories;

namespace Domain.Repositories;

public interface ISolicitudeTypeRepository : IGenericRepository<SolicitudeType>
{
  Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}