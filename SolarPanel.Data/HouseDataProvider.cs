using System.Reflection;
using Newtonsoft.Json;
using SolarPanel.Types;

namespace SolarPanel.Data
{
    public class HouseProvider
    {
        #region Singleton Implementation

        private static readonly Lazy<HouseProvider> lazy =
        new Lazy<HouseProvider>(() => new HouseProvider());

        public static HouseProvider Instance { get { return lazy.Value; } }

        #endregion

        #region Constructors

        private HouseProvider()
        {
            var dataFileLocation = AppDomain.CurrentDomain.BaseDirectory + "Houses.json";

            Console.WriteLine($"Loading data file from {dataFileLocation}");
            var dataFile = File.ReadAllText(dataFileLocation);

            var data = JsonConvert.DeserializeObject<List<House>>(dataFile);
            
            Houses = data ?? new List<House>();
        }

        #endregion

        public void SaveToFile(string path)
        {
            var file = JsonConvert.SerializeObject(Houses);
        }

        /// <summary>
        /// All the houses available
        /// </summary>
        public List<House> Houses { get; }
    }
}