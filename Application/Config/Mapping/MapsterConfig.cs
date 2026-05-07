using Application.Auth.Register;
using Application.Clients.CreatePQRSDF;
using Application.Clients.GetPQRSDF;
using Application.Shared.Dtos;
using Application.Users.ChangePQRSDFState;
using Application.Users.GetPQRSDFDetails;
using Application.Users.MakePQRSDFResponse;
using Domain.Entities.Clients;
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

    // Solicitude response mappings
    TypeAdapterConfig<SolicitudeResponse, PQRSDFResponseDto>.NewConfig().TwoWays();
    TypeAdapterConfig<SolicitudeResponse, MakePQRSDFCommandDto>.NewConfig().TwoWays();
    TypeAdapterConfig<SolicitudeResponse, SolicitudeRespondeDto>.NewConfig().TwoWays();


    // User mappings
    TypeAdapterConfig<User, RegisterCommandDto>.NewConfig().TwoWays();

    // Client
    TypeAdapterConfig<Client, ClientCommandDto>.NewConfig().TwoWays();
  }
}