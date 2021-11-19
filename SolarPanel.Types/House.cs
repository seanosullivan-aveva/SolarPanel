using System;
using System.Drawing;

namespace SolarPanel.Types;

public class House
{
    public House(
        string id,
        SizeF roofSize,
        float daylightElectricityConsumption,
        float electricityCost)
    {
        Id = id;
        RoofSize = roofSize;
        DaylightElectricityConsumption = daylightElectricityConsumption;
        ElectricityCost = electricityCost;
    }

    /// <summary>
    /// The Size of the roof (m x m)
    /// </summary>
    public SizeF RoofSize {get;}

    /// <summary>
    /// The Id of the house
    /// </summary>
    public string Id {get;}

    /// <summary>
    /// The amount (in Watts) of electricity consumed in daylight hours
    /// </summary>
    public float DaylightElectricityConsumption {get;}

    // The price the house purchases electricity for ($/kWh)
    public float ElectricityCost { get; }
}
