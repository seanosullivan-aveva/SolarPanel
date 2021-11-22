using System.Drawing;

namespace SolarPanel.Types;

public class Panel
{
    public Panel(
        string manufacturer,
        string model,
        SizeF size,
        float power,
        float efficiency,
        float weight,
        float cost,
        string url)
    {
        Manufacturer = manufacturer;
        Model = model;
        Size = size;
        Power = power;
        Efficiency = efficiency;
        Weight = weight;
        Cost = cost;
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
    public SizeF Size { get; }

    /// <summary>
    /// The nominal power of the panel (Watts)
    /// </summary>
    public float Power { get; }

    /// <summary>
    /// The efficiency of the panel (%)
    /// </summary>
    public float Efficiency { get; }

    /// <summary>
    /// The weight of the panel (Kg)
    /// </summary>
    public float Weight { get; }

    /// <summary>
    /// The purchase cost of the panel (£)
    /// </summary>
    public float Cost { get; }

    /// <summary>
    /// The URL of the product details
    /// </summary>
    public string Url {get;}
}
