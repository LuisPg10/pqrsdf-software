using Domain.Primitives;
using Domain.Repositories;
using Infraestructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Services;

public static class DependencyInjection
{
  public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
  {
    // DbContext config
    services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(configuration.GetConnectionString("Database"))
    );

    services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IClientRepository, ClientRepository>();
    services.AddScoped<ISolicitudeRepository, SolicitudeRepository>();

    return services;
  }
}