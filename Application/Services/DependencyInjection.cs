using Application.Config.Mapping;
using Application.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    // MediatR config
    services.AddMediatR(config => { config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>(); });

    services.AddHttpContextAccessor();

    // Mapster config
    MapsterConfig.RegisterMappings();

    services.AddScoped<ITokenHandler, TokenHandler>();

    return services;
  }
}