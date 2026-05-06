using Application.Auth.Register;
using Application.Clients.CreatePQRSDF;
using Application.Users.GetPQRSDFDetails;
using Domain.Entities.Solicitudes;
using Domain.Entities.Users;

namespace Application.Config.Mapping;

public static class MapsterConfig
{
  public static void RegisterMappings()
  {
    // Solicitude mappings
    TypeAdapterConfig<Solicitude, CreatePQRSDFCommandDto>.NewConfig().TwoWays();
    TypeAdapterConfig<Solicitude, DetailsSolicitudeDto>.NewConfig().TwoWays();

    // User mappings
    TypeAdapterConfig<User, RegisterCommandDto>.NewConfig().TwoWays();
  }
}