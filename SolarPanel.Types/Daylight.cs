public class Daylight
{
    public Daylight(int month, float hoursOfDaylightPerDay)
    {
        Month = month;
        HoursOfDaylightPerDay = hoursOfDaylightPerDay;
    }

    /// <summary>
    /// The index of the month
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// The average number of hours of daylight per day in that month
    /// </summary>
    public float HoursOfDaylightPerDay { get; }

    /// <summary>
    /// The average number of hours of usable daylight per day in that month
    /// </summary>
    public float HoursOfUsableDaylightPerDay => HoursOfDaylightPerDay - 2;
}