using System.Drawing;
using SolarPanel.Types;

public static class TestUtilities
{
    public static Tariff CreateTariff(double price, DateTime expiry, double expiredPrice)
    {
        return new Tariff("Test", price, expiry, expiredPrice, null, null);
    }

    public static Panel CreateTestPanel(SizeF size, float power = 0.0f)
    {
        return new Panel(
            "test",
            "test",
            size,
            power,
            0.0f,
            0.0f,
            0.0f,
            0.0f,
            string.Empty);
    }

    public static House CreateTestHouse(SizeF size)
    {
        return new House("Test", size, 0, 0);
    }
}