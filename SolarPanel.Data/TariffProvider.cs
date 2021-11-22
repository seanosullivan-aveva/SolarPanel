using System.Reflection;
using Newtonsoft.Json;
using SolarPanel.Types;

namespace SolarPanel.Data;

public class TariffProvider
{
    #region Singleton Implementation

    private static readonly Lazy<TariffProvider> lazy =
    new Lazy<TariffProvider>(() => new TariffProvider());

    public static TariffProvider Instance { get { return lazy.Value; } }

    #endregion

    #region Constructors

    private TariffProvider()
    {
        var dataFileLocation = AppDomain.CurrentDomain.BaseDirectory + "Tariffs.json";

        Console.WriteLine($"Loading data file from {dataFileLocation}");
        var dataFile = File.ReadAllText(dataFileLocation);

        var data = JsonConvert.DeserializeObject<List<Tariff>>(dataFile);
        
        Tariffs = data ?? new List<Tariff>();
    }

    #endregion

    public void SaveToFile(string path)
    {
        var file = JsonConvert.SerializeObject(Tariffs);
    }
    
    /// <summary>
    /// All the solar Tariffs available
    /// </summary>
    public List<Tariff> Tariffs { get; }
}
