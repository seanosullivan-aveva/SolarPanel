using System.Drawing;

namespace SolarPanel.Types;

public class Panel
{

    public Panel(
        string manufacturer,
        string model,
        SizeF panelSize,
        double power,
        double efficiency,
        double weight,
        double cost,
        double installationCost,
        string url)
    {
        Manufacturer = manufacturer;
        Model = model;
        PanelSize = panelSize;
        Power = power;
        Efficiency = efficiency;
        Weight = weight;
        Cost = cost;
        InstallationCost = installationCost;
        Url = url;
    }

    /// <summary>
    /// The name of the company that manufactures the panel
    /// </summary>
    public string Manufacturer { get; }

    /// <summary>
    /// The model of the solar panel
    /// </summary>
    public string Model { get; }

    /// <summary>
    /// The size of the panel (in metres)
    /// </summary>
    public SizeF PanelSize { get; }

    /// <summary>
    /// The nominal power of the panel (Watts)
    /// </summary>
    public double Power { get; }

    /// <summary>
    /// The efficiency of the panel (%)
    /// </summary>
    public double Efficiency { get; }

    /// <summary>
    /// The weight of the panel (Kg)
    /// </summary>
    public double Weight { get; }

    /// <summary>
    /// The purchase cost of the panel (£)
    /// </summary>
    public double Cost { get; }

    /// <summary>
    /// The const of installation of the panel (£)
    /// </summary>
    public double InstallationCost { get; }

    /// <summary>
    /// The URL of the product details
    /// </summary>
    public string Url {get;}
}
