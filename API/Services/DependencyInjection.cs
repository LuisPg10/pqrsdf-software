using System.Text;
using API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddTransient<GlobalExceptionHandlingMiddleware>();

    var secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      if (secretKey != null)
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
          ValidateIssuer = false,
          ValidateAudience = false,
        };
    });

    return services;
  }
}