using SolarPanel.Types;

namespace SolarPanel.Logic;

public static class HouseUtilities
{
    /// <summary>
    /// Computes the maximum number of panels that can be fitted to the house
    /// </summary>
    /// <param name="house">The house to examine</param>
    /// <param name="panel">The panel to fit to the roof</param>
    /// <returns>The maximum number of panels that can be fitted to the roof</returns>
    public static int MaxNumberOfPanels(House house, Panel panel)
    {
        double pWidth = panel.Size.Width;
        double rWidth = house.RoofSize.Width;
        double pHeight = panel.Size.Height;
        double rHeight = house.RoofSize.Height;

        // Check that the dimensions have sensible values
        if(pWidth < 0 || pHeight < 0 || rWidth < 0 || rHeight < 0)
        {
            return 0;
        }

        // Check that we can get at least one panel on
        if(pWidth > rWidth || pHeight > rHeight)
        {
            return 0;
        }

        // Compute the number of panels that can fitted         
        int hPanels = (int)(rWidth/pWidth);
        int vPanels = (int)(rHeight/pHeight);

        return hPanels * vPanels;
    }
}
