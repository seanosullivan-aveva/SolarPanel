using SolarPanel.Application;
using SolarPanel.Data;
using SolarPanel.Logic.Utilities;

public class Implementation
{
    public static void ReadData()
    {
        foreach(var solarPanel in PanelProvider.Instance.SolarPanels)
        {
            Console.WriteLine($"Model:{solarPanel.Model} - {solarPanel.Manufacturer}");
        }
    }

    public static void KnownHouseAndPanel()
    {
        var house = HouseProvider.Instance.Houses.First(o => o.Id == "Seans House");
        var panel = PanelProvider.Instance.SolarPanels.First(o => o.Model == "PowerXT® Pure Black™400W");
        var tariff = TariffProvider.Instance.Tariffs.First(o => o.Name == "Test");
        var installer = InstallerProvider.Instance.Installers.First();

        int numYears = 25;
        var result = CalculationEngine.Compute(house, panel, tariff, installer, numYears);

        if (result.BreakEvenDate.HasValue && result.TimeToBreakEven.HasValue)
        {
            Console.WriteLine("========================================================================");
            Console.WriteLine("Success");
            Console.WriteLine($"Break even date: {result.BreakEvenDate.Value.ToShortDateString()}");
            Console.WriteLine($"Time To Break Even: {result.TimeToBreakEven.Value.TotalDays:f0} days");
        }
        else
        {
            Console.WriteLine("Failure");
            Console.WriteLine($"After {numYears} we failed to cover the costs of the solar power system");
        }

        Console.WriteLine("========================================================================");

        if (result.TotalProfit >= 0f)
        {
            Console.WriteLine($"Over {numYears} years the system will generate £{result.TotalProfit:f0} profit");
        }
        else
        {
            Console.WriteLine($"Over {numYears} years the system will generate £{-result.TotalProfit:f0} loss");
        }

        Console.WriteLine($"Solar Panel Cost: £{result.PanelCost:f0}");
        Console.WriteLine($"Number of panels installed: {result.PanelCount}");
        Console.WriteLine($"Installation Cost: £{result.InstallationCost:f0}");
        Console.WriteLine($"Total Initial Outlay: £{result.TotalInitialOutlay:f0}");

        if (result.SavedOnBills > 0)
        {
            Console.WriteLine($"Over {numYears} years the system will save £{result.SavedOnBills:f0} on electricity bills");
        }
        else
        {
            Console.WriteLine($"Over {numYears} years the system won't generate enough electricity to sell");
        }

        if (result.GenerationProfit > 0)
        {
            Console.WriteLine($"Over {numYears} years the system will generate £{result.GenerationProfit:f0} from selling electricity");
        }
        else
        {
            Console.WriteLine($"Over {numYears} years the system won't generate enough electricity to sell");
        }
        Console.WriteLine("========================================================================");
    }
}