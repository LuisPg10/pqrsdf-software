using Application.Config.Mapping;
using Application.Utilities;
using Application.Utilities.CurrentUsers;
using Application.Utilities.Holidays;
using Application.Utilities.TokenHandlers;
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
    services.AddScoped<IHolidayService, HolidayService>();
    services.AddScoped<ICurrentUserService, CurrentUserService>();

    return services;
  }
}