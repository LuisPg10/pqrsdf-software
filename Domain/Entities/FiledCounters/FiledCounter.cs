using Domain.Generics;

namespace Domain.Entities.FiledCounters;

public class FiledCounter : GenericEntity
{
  public int Year { get; private set; }
  public int LastNumber { get; private set; }

  public static FiledCounter Create(int year)
  {
    return new FiledCounter
    {
      Id = Guid.NewGuid(),
      Year = year,
      LastNumber = 0,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };
  }

  public int GetNextNumber()
  {
    LastNumber++;
    UpdatedAt = DateTime.UtcNow;
    return LastNumber;
  }
}