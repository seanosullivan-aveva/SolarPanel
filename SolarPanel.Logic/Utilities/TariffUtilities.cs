using SolarPanel.Types;

public static class TariffUtilities
{
    /// <summary>
    /// Computes the amount paid for the generation
    /// </summary>
    /// <param name="tariff">The generation tariff</param>
    /// <param name="powerGenerated">The amount of power generated (Watts)</param>
    /// <param name="when">When the power was (or will be) generated</param>
    /// <param name="forHowLong">Over how long was the power generated</param>
    /// <returns>The amount the energy supplier will pay (in pounds)</returns>
    public static double ComputeAmountPaid(Tariff tariff, double powerGenerated, DateTime when, TimeSpan forHowLong)
    {
        double wattHours = powerGenerated * forHowLong.TotalHours;

        return ComputeAmountPaid(tariff, wattHours, when);
    }

    /// <summary>
    /// Computes the amount paid for generation
    /// </summary>
    /// <param name="tariff">The generation tariff</param>
    /// <param name="powerGenerated">The amount of power generated (Watt Hours)</param>
    /// <param name="when">When the power was (or will be generated)</param>
    /// <returns>The amount the energy supplier will pay (in pounds)</returns>
    public static double ComputeAmountPaid(Tariff tariff, double powerGenerated, DateTime when)
    {
       double tariffRate;
        if(when < tariff.Expiry)
        {
            tariffRate = tariff.Price;
        }
        else
        {
            tariffRate = tariff.ExpiredPrice;
        }

        double tariffRateInPounds = tariffRate / 100.0;
        double powerGeneratedKilowattHours = powerGenerated / 1000.0;
        return powerGeneratedKilowattHours * tariffRateInPounds;
    }
}