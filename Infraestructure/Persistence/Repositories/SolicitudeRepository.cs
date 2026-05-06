using Domain.Entities.Solicitudes;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class SolicitudeRepository(ApplicationDbContext db) : GenericRepository<Solicitude>(db), ISolicitudeRepository
{
  public async Task<Solicitude?> GetSolicitudeByFiledNumber(string filedNumber)
  {
    return await db.Solicitudes.FirstOrDefaultAsync(s => s.FiledNumber == filedNumber);
  }
}