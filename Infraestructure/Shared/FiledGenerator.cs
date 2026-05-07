using Application.Utilities.IFiledGenerators;
using Domain.Entities.FiledCounters;

namespace Infraestructure.Shared;

public class FiledGenerator(ApplicationDbContext db) : IFiledGenerator
{
  public async Task<string> GenerateFiledAsync(CancellationToken cancellationToken = default)
  {
    var currentYear = DateTime.UtcNow.Year;

    await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

    try
    {
      var sequence = await db.Set<FiledCounter>()
        .FirstOrDefaultAsync(s => s.Year == currentYear, cancellationToken);

      if (sequence == null)
      {
        sequence = FiledCounter.Create(currentYear);
        await db.Set<FiledCounter>().AddAsync(sequence, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
      }

      var nextNumber = sequence.GetNextNumber();
      await db.SaveChangesAsync(cancellationToken);

      await transaction.CommitAsync(cancellationToken);

      return $"{currentYear}-{nextNumber:D8}";
    }
    catch
    {
      await transaction.RollbackAsync(cancellationToken);
      throw;
    }
  }
}