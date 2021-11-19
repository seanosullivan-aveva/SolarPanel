using System;
using System.Drawing;

namespace HouseData;

public class House
{
    public House(
        string id,
        SizeF roofSize)
    {
        Id = id;
        RoofSize = roofSize;
    }

    /// <summary>
    /// The Size of the roof (m x m)
    /// </summary>
    public SizeF RoofSize {get;}

    /// <summary>
    /// The Id of the house
    /// </summary>
    public string Id {get;}
}
