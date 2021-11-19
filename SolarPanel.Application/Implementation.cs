using SolarPanel.Data;

public class Implementation
{
    public static void ReadData()
    {
        foreach(var solarPanel in PanelProvider.Instance.SolarPanels)
        {
            Console.WriteLine($"Model:{solarPanel.Model} - {solarPanel.Manufacturer}");
        }
    }
}