using System;

/// <summary>
/// A single class that provides useful date capabilities
/// </summary>
public class DateProvider
{
    #region Singleton Implementation

    private static readonly Lazy<DateProvider> lazy =
    new Lazy<DateProvider>(() => new DateProvider());

    public static DateProvider Instance { get { return lazy.Value; } }

    #endregion

    #region Constructor

    private DateProvider()
    {

    }

    #endregion

    /// <summary>
    /// Gets all the days from the time specified for the period of time specified
    /// </summary>
    /// <param name="from">Start date</param>
    /// <param name="forHowLong">For how long to enumerate for</param>
    /// <returns>An enumeration of all the days from the start specified for the time span specified</returns>
    public IEnumerable<DateTime> GetDays(DateTime from, TimeSpan forHowLong)
    {
        var increment = TimeSpan.FromDays(1);
        var stop = from + forHowLong;
        var current = from;

        while(current < stop)
        {
            yield return current;
            current += increment;
        }
    }
}