using Infraestructure.Config.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class MigratioExtensions
{
  public static void ApplyMigrations(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
  }
}