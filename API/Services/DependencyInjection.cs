using API.Middlewares;

namespace API.Services;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddTransient<GlobalExceptionHandlingMiddleware>();

    return services;
  }
}