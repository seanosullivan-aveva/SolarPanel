using SolarPanelData;

public class Implementation
{
    public static void ReadData()
    {
        foreach(var solarPanel in DataProvider.Instance.SolarPanels)
        {
            Console.WriteLine($"Model:{solarPanel.Model} - {solarPanel.Manufacturer}");
        }
    }
}