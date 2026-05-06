using Application.Clients.CreatePQRSDF;
using Domain.Entities.Solicitudes;
using Mapster;

namespace Application.Config.Mapping;

public static class MapsterConfig
{
  public static void RegisterMappings()
  {
    // User mappings
    TypeAdapterConfig<Solicitude, CreatePQRSDFCommandDto>.NewConfig().TwoWays();
  }
}