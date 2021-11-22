using System.Reflection;
using Newtonsoft.Json;
using SolarPanel.Types;

namespace SolarPanel.Data;

public class PanelProvider
{
    #region Singleton Implementation

    private static readonly Lazy<PanelProvider> lazy =
    new Lazy<PanelProvider>(() => new PanelProvider());

    public static PanelProvider Instance { get { return lazy.Value; } }

    #endregion

    #region Constructors

    private PanelProvider()
    {
        var dataFileLocation = AppDomain.CurrentDomain.BaseDirectory + "Panels.json";

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
