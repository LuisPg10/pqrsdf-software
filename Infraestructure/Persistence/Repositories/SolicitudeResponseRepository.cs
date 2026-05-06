using Domain.Entities.SolicitudeResponses;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class SolicitudeResponseRepository(ApplicationDbContext db)
  : GenericRepository<SolicitudeResponse>(db), ISolicitudeResponseRepository;