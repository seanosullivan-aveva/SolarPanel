using System.Reflection;
using Newtonsoft.Json;
using SolarPanel.Types;

namespace SolarPanel.Data
{
    /// <summary>
    /// File data take from: https://projectbritain.com/weather/sunshine.htm
    /// </summary>
    public class DaylightProvider
    {
        #region Singleton Implementation

        private static readonly Lazy<DaylightProvider> lazy =
        new Lazy<DaylightProvider>(() => new DaylightProvider());

        public static DaylightProvider Instance { get { return lazy.Value; } }

        #endregion

        private Dictionary<int, float> m_setMonthToUsableDaylightHours;

        #region Constructors

        private DaylightProvider()
        {
            var dataFileLocation = AppDomain.CurrentDomain.BaseDirectory + "Daylight.json";
            var dataFile = File.ReadAllText(dataFileLocation);

            var data = JsonConvert.DeserializeObject<List<Daylight>>(dataFile);
            
            Daylights = data ?? new List<Daylight>();
            m_setMonthToUsableDaylightHours = Daylights.ToDictionary(o => o.Month, o => o.HoursOfUsableDaylightPerDay);
        }

        #endregion

        public void SaveToFile(string path)
        {
            var file = JsonConvert.SerializeObject(Daylights);
        }

        /// <summary>
        /// All the houses available
        /// </summary>
        public List<Daylight> Daylights { get; }

        /// <summary>
        /// Gets the amount of usable daylight per day for the month specified
        /// </summary>
        /// <param name="month">The month</param>
        /// <returns>The amount of usable daylight per day for the month specified</returns>
        public float GetUsableDaylight(int month)
        {
            if(month < 1 || month > m_setMonthToUsableDaylightHours.Count)
            {
                throw new ArgumentOutOfRangeException($"The month specified is out of range {month}");
            }

            return m_setMonthToUsableDaylightHours[month];
        }
    }
}