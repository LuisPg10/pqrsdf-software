using Domain.Entities.Clients;
using Domain.Entities.Solicitudes;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Infraestructure.Config.EntityFramework;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
  : DbContext(options), IUnitOfWork
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // tables configuration
    modelBuilder.Entity<Client>(entity => entity.Property(client => client.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<Solicitude>(entity => entity.Property(s => s.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<User>(entity => entity.Property(u => u.Id).ValueGeneratedOnAdd());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    var eventosDeDominio = ChangeTracker.Entries<AggregateRoot>()
      .Select(e => e.Entity)
      .Where(e => e.GetDomainEvents().Count != 0)
      .SelectMany(e => e.GetDomainEvents());

    var resultado = await base.SaveChangesAsync(cancellationToken);

    foreach (var evento in eventosDeDominio)
    {
      await publisher.Publish(evento, cancellationToken);
    }

    return resultado;
  }

  public DbSet<Client> Clients { get; set; }
  public DbSet<Solicitude> Solicitudes { get; set; }
  public DbSet<User> Users { get; set; }
}