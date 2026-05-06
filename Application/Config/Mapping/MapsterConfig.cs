using Application.Auth.Register;
using Application.Clients.CreatePQRSDF;
using Application.Users.ChangePQRSDFState;
using Application.Users.GetPQRSDFDetails;
using Domain.Entities.SolicitudeResponses;
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
    TypeAdapterConfig<Solicitude, ChangePQRSDFStateCommandDto>.NewConfig().TwoWays();
    
    // User mappings
    TypeAdapterConfig<User, RegisterCommandDto>.NewConfig().TwoWays();
  }
}