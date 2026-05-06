using Application.Auth.Register;
using Application.Clients.CreatePQRSDF;
using Domain.Entities.Solicitudes;
using Domain.Entities.Users;

namespace Application.Config.Mapping;

public static class MapsterConfig
{
  public static void RegisterMappings()
  {
    // User mappings
    TypeAdapterConfig<Solicitude, CreatePQRSDFCommandDto>.NewConfig().TwoWays();
    TypeAdapterConfig<User, RegisterCommandDto>.NewConfig().TwoWays();
  }
}