namespace Application.Utilities.Holidays
{
  public static class BusinessDayCalculator
  {
    public static DateTime AddBusinessDays(DateTime startDate, int businessDays, IEnumerable<DateTime> holidays)
    {
      if (businessDays < 0) throw new ArgumentOutOfRangeException(nameof(businessDays), "businessDays must be >= 0");
      holidays ??= Array.Empty<DateTime>();
      var holidaySet = new HashSet<DateTime>(holidays.Select(h => h.Date));

      if (businessDays == 0) return startDate.Date;

      var current = startDate.Date;
      var added = 0;
      while (added < businessDays)
      {
        current = current.AddDays(1);
        if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday) continue;
        if (holidaySet.Contains(current)) continue;
        added++;
      }

      return current;
    }

    public static bool IsHoliday(DateTime date, IEnumerable<DateTime> holidays)
    {
      holidays ??= Array.Empty<DateTime>();
      var holidaySet = new HashSet<DateTime>(holidays.Select(h => h.Date));
      return date.DayOfWeek == DayOfWeek.Saturday
             || date.DayOfWeek == DayOfWeek.Sunday
             || holidaySet.Contains(date.Date);
    }
  }

  public class HolidayService : IHolidayService
  {
    private readonly HashSet<DateTime> _holidays;

    public HolidayService()
    {
      _holidays = GetColombianHolidays(2026);
    }

    public bool IsHoliday(DateTime date)
    {
      return BusinessDayCalculator.IsHoliday(date, _holidays);
    }

    public DateTime AddBusinessDays(DateTime startDate, int businessDays)
    {
      return BusinessDayCalculator.AddBusinessDays(startDate, businessDays, _holidays);
    }

    private static HashSet<DateTime> GetColombianHolidays(int year)
    {
      var list = new List<DateTime>
      {
        new(year, 1, 1), // Año Nuevo
        new(year, 1, 12), // Reyes Magos (observed)
        new(year, 3, 23), // San José (observed)
        new(year, 4, 2), // Jueves Santo (observed)
        new(year, 4, 3), // Viernes Santo (observed)
        new(year, 5, 1), // Día del Trabajo
        new(year, 5, 25), // Ascensión (observed)
        new(year, 6, 15), // Corpus Christi (observed)
        new(year, 6, 22), // Sagrado Corazón (observed)
        new(year, 7, 20), // Independencia
        new(year, 8, 7), // Batalla de Boyacá
        new(year, 8, 17), // Asunción (observed)
        new(year, 10, 12), // Día de la Raza (observed)
        new(year, 11, 2), // Todos los Santos (observed)
        new(year, 11, 16), // Independencia de Cartagena (observed)
        new(year, 12, 8), // Inmaculada Concepción
        new(year, 12, 25), // Navidad
        new(year, 5, 18) // ejemplo adicional (puedes reemplazar por otro festivo observado)
      };

      return new HashSet<DateTime>(list.Select(d => d.Date));
    }
  }
}