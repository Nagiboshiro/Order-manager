namespace OrderManager.Application.Extensions;

public static class DateTimeExtensions
{
    public static bool IsInRange(DateTimeOffset dateToCheck, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        return dateToCheck >= startDate && dateToCheck < endDate;
    }
}