using Domain.Entities.Solicitudes;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class SolicitudeRepository(ApplicationDbContext db) : GenericRepository<Solicitude>(db), ISolicitudeRepository
{
  public override IQueryable<Solicitude> ListAll()
  {
    return base.ListAll()
      .Include(so => so.Client)
      .Include(so => so.Type)
      .Include(so => so.Area)
      .Include(so => so.Responses)
      .Include(so => so.Traceabilities);
  }

  public async Task<Solicitude?> GetSolicitudeByFiledNumber(string filedNumber)
  {
    return await db.Solicitudes.FirstOrDefaultAsync(s => s.FiledNumber == filedNumber);
  }
}