namespace SolarPanel.Types;

public class Tariff
{
    public Tariff(
        string name,
        float price,
        DateTime expiry,
        float expiredPrice,
        float? minimumFeedAmount,
        float? maximumFeedAmount)
    {
        Name = name;
        Price = price;
        Expiry = expiry;
        ExpiredPrice = expiredPrice;
        MinimumFeedAmount = minimumFeedAmount;
        MaximumFeedAmount = maximumFeedAmount;
    }

    /// <summary>
    /// The name of the tariff
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The feed in price (£/kWh)
    /// </summary>
    public float Price { get; }

    /// <summary>
    /// The expiry date of the tariff
    /// </summary>
    public DateTime Expiry { get; }

    /// <summary>
    /// The feed in price after the expiry date has expired
    /// </summary>
    public float ExpiredPrice { get; }

    /// <summary>
    /// The minimum amount (kWh) that must be supplied to qualify for this tariff
    /// </summary>
    /// <value></value>
    public float? MinimumFeedAmount { get; }

    /// <summary>
    /// The maximum amount (kWh) that must be supplies to qualify for this tariff
    /// </summary>
    /// <value></value>
    public float? MaximumFeedAmount { get; }
}
