namespace SolarPanel.Types;

public class Installer
{
    public Installer(
        string id,
        float callOutCost,
        float costPerPanel)
    {
        Id = id;
        CallOutCost = callOutCost;
        CostPerPanel = costPerPanel;
    }

    /// <summary>
    /// The Id of the house
    /// </summary>
    public string Id {get;}

    /// <summary>
    /// The one time call out cost of this installer (£)
    /// </summary>
    public float CallOutCost { get; }

    /// <summary>
    /// The installation cost per panel (£)
    /// </summary>
    public float CostPerPanel { get; }

}
