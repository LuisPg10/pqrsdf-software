using Domain.Entities.Solicitudes;
using Domain.Repositories;
using Infraestructure.Shared.Repositories;

namespace Infraestructure.Persistence.Repositories;

public class SolicitudeRepository(ApplicationDbContext db) : GenericRepository<Solicitude>(db), ISolicitudeRepository;