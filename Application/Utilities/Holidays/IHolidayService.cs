namespace Application.Utilities.Holidays
{
    public interface IHolidayService
    {
        bool IsHoliday(DateTime date);
        DateTime AddBusinessDays(DateTime startDate, int businessDays);
    }
}
