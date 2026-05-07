using Domain.Entities.Areas;
using Domain.Entities.Clients;
using Domain.Entities.SolicitudeResponses;
using Domain.Entities.Solicitudes;
using Domain.Entities.Traceabilities;
using Domain.Entities.Users;
using Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestructure.Config.EntityFramework;

public class ApplicationDbContext(
  DbContextOptions<ApplicationDbContext> options,
  IPublisher publisher,
  IHttpContextAccessor httpContextAccessor)
  : DbContext(options), IUnitOfWork
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      foreach (var property in entityType.GetProperties())
      {
        if (!property.ClrType.IsEnum) continue;

        var converterType = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
        var converter = (ValueConverter)Activator.CreateInstance(converterType)!;
        property.SetValueConverter(converter);
      }
    }

    // tables configuration
    modelBuilder.Entity<Client>(entity => entity.Property(client => client.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<Solicitude>(entity => entity.Property(s => s.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<SolicitudeResponse>(entity => entity.Property(s => s.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<SolicitudeType>(entity => entity.Property(s => s.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<User>(entity => entity.Property(u => u.Id).ValueGeneratedOnAdd());
    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

    // Configuraci�n de Traceability
    modelBuilder.Entity<Traceability>(entity =>
    {
      entity.ToTable("Traceabilities");
      entity.HasKey(t => t.Id);
      entity.Property(t => t.Action).HasMaxLength(100).IsRequired();
      entity.Property(t => t.Description).HasMaxLength(500);
      entity.Property(t => t.User).HasMaxLength(100).IsRequired();
      entity.HasIndex(t => t.SolicitudeId);
      entity.HasIndex(t => t.CreationDate);
    });
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    var user = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "system";
    foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
    {
      entry.Entity.SetOperationContext(user);
    }

    var domainEvents = ChangeTracker.Entries<AggregateRoot>()
      .Select(e => e.Entity)
      .Where(e => e.GetDomainEvents().Count != 0)
      .SelectMany(e => e.GetDomainEvents())
      .ToList();

    var traces = new List<Traceability>();
    foreach (var domainEvent in domainEvents)
    {
      var trace = Traceability.CreateFromDomainEvent(domainEvent, GetSolicitudeIdFromEvent(domainEvent));
      if (trace != null)
      {
        traces.Add(trace);
      }
    }

    if (traces.Any())
    {
      await Set<Traceability>().AddRangeAsync(traces, cancellationToken);
    }

    var resultado = await base.SaveChangesAsync(cancellationToken);

    foreach (var domainEvent in domainEvents)
    {
      await publisher.Publish(domainEvent, cancellationToken);
    }

    foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
    {
      entry.Entity.ClearDomainEvents();
    }

    return resultado;
  }

  private Guid GetSolicitudeIdFromEvent(dynamic domainEvent)
  {
    return domainEvent.SolicitudeId;
  }

  public DbSet<Client> Clients { get; set; }
  public DbSet<Solicitude> Solicitudes { get; set; }
  public DbSet<SolicitudeResponse> SolicitudeResponses { get; set; }
  public DbSet<SolicitudeType> SolicitudeTypes { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Area> Areas { get; set; }
  public DbSet<Traceability> Traceabilities { get; set; }
}