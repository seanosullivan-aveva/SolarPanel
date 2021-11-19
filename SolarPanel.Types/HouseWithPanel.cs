using SolarPanel.Types;

/// <summary>
/// Represents a house with some panels attached
/// </summary>
public class HouseWithPanel
{
    public HouseWithPanel(House house, Panel panel, int numberOfPanels)
    {
        House = house;
        Panel = panel;
        NumberOfPanels = numberOfPanels;
    }

    /// <summary>
    /// The house information
    /// </summary>
    public House House { get; }

    /// <summary>
    /// The panel information
    /// </summary>
    public Panel Panel { get; }

    /// <summary>
    /// The number of panels fitted
    /// </summary>
    public int NumberOfPanels { get; }
}