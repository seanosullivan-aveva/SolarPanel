namespace SolarPanel.Types;

public class Tariff
{
    public Tariff(
        string name,
        double price,
        DateTime expiry,
        double expiredPrice,
        double? minimumFeedAmount,
        double? maximumFeedAmount)
    {
        Name = name;
        Price = price;
        Expiry = expiry;
        ExpiredPrice = expiredPrice;
        MinimumFeedAmount = minimumFeedAmount;
        MaximumFeedAmount = maximumFeedAmount;
    }

    public string Name { get; }

    public double Price { get; }

    public DateTime Expiry { get; }

    public double ExpiredPrice { get; }

    public double? MinimumFeedAmount { get; }

    public double? MaximumFeedAmount { get; }
}
