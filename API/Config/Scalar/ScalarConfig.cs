using Scalar.AspNetCore;

namespace API.Config.Scalar;

public static class ScalarConfig
{
  public static void Config(WebApplication app)
  {
    app.MapScalarApiReference("/docs", options =>
    {
      options
        .WithTitle("PQRSDF Documentation")
        // .AddPreferredSecuritySchemes("BearerAuth")
        // .AddHttpAuthentication("BearerAuth", auth => auth.Token = "")
        .WithClassicLayout()
        .DisableAgent();
    });
  }
}