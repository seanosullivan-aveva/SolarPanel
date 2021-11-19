using System.Reflection;
using Newtonsoft.Json;
using SolarPanel.Types;

namespace SolarPanel.Data;

public class DataProvider
{
    #region Singleton Implementation

    private static readonly Lazy<DataProvider> lazy =
    new Lazy<DataProvider>(() => new DataProvider());

    public static DataProvider Instance { get { return lazy.Value; } }

    #endregion

    #region Constructors

    private DataProvider()
    {
        var dataFileLocation = AppDomain.CurrentDomain.BaseDirectory + "Panels.json";

        Console.WriteLine($"Loading data file from {dataFileLocation}");
        var dataFile = File.ReadAllText(dataFileLocation);

        var data = JsonConvert.DeserializeObject<List<Panel>>(dataFile);
        
        SolarPanels = data ?? new List<Panel>();
    }

    #endregion

    public void SaveToFile(string path)
    {
        var file = JsonConvert.SerializeObject(SolarPanels);
    }

    /// <summary>
    /// All the solar panels available
    /// </summary>
    public List<Panel> SolarPanels { get; }
}
