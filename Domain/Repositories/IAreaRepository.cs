using Domain.Entities.Areas;
using Domain.Shared.Repositories;

namespace Domain.Repositories
{
  public interface IAreaRepository : IGenericRepository<Area>
  {
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
  }
}