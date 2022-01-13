using SolarPanel.Application;
using SolarPanel.Data;
using System;

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
        var tariff = TariffProvider.Instance.Tariffs.First(o => o.Name == "PVA - Standard - Ultra Small - Perpetual");
        var installer = InstallerProvider.Instance.Installers.First();

        int numYears = 25;
        var economics = CalculationEngine.Compute(house, panel, tariff, installer, numYears);

        OutputEconomics(economics, numYears);
    }

    public static void FindMeBestTariff()
    {
        int numYears = 25;
        var house = HouseProvider.Instance.Houses.First(o => o.Id == "Seans House");
        var panel = PanelProvider.Instance.SolarPanels.First(o => o.Model == "PowerXT® Pure Black™400W");
        var tariff = TariffProvider.Instance.Tariffs.First(o => o.Name == "Test");
        var installer = CalculationEngine.FindCheapestInstaller(house, panel);

        if(installer == null)
        {
            Console.WriteLine("Failed to find an installer");
            return;
        }

 
        var economics = CalculationEngine.FindBestTariff(house, panel, installer, numYears);

        if(economics == null)
        {
            Console.WriteLine("Failed to compute a suitable tariff");
            return;
        }
        Console.WriteLine("========================================================================");
        Console.WriteLine($"The best installer is {installer.Id}");
        Console.WriteLine($"The best tariff is {economics.Value.tariff.Name}");

        OutputEconomics(economics.Value.economics, numYears);
    }

    public static void FindMeBestCombinationOfPanelAndTariff(string houseName)
    {
        var house = HouseProvider.Instance.Houses.First(o => o.Id == houseName);
        int numYears = 25;

        var best = CalculationEngine.FindBestCombination(house, numYears);

        if (best == null)
        {
            Console.WriteLine($"Failed to compute a suitable combination of panel and tariff for the house {house.Id}");
            return;
        }

        Console.WriteLine("========================================================================");
        Console.WriteLine($"The best installer is {best.Value.installer.Id}");
        Console.WriteLine($"The best panel is {best.Value.panel.Model} made by {best.Value.panel.Manufacturer}");
        Console.WriteLine($"The best tariff is {best.Value.tariff.Name}");

        OutputEconomics(best.Value.economics, numYears);

    }

    private static void OutputEconomics(Economics economics, int numYears)
    {
        if (economics.BreakEvenDate.HasValue && economics.TimeToBreakEven.HasValue)
        {
            Console.WriteLine("========================================================================");
            Console.WriteLine("Success");
            Console.WriteLine($"Break even date: {economics.BreakEvenDate.Value.ToShortDateString()}");
            Console.WriteLine($"Time To Break Even: {economics.TimeToBreakEven.Value.TotalDays:f0} days");
        }
        else
        {
            Console.WriteLine("Failure");
            Console.WriteLine($"After {numYears} years we failed to cover the costs of the solar power system");
        }

        Console.WriteLine("========================================================================");

        if (economics.TotalProfit >= 0f)
        {
            Console.WriteLine($"Over {numYears} years the system will generate £{economics.TotalProfit:f0} profit");
        }
        else
        {
            Console.WriteLine($"Over {numYears} years the system will generate £{-economics.TotalProfit:f0} loss");
        }

        Console.WriteLine($"Solar Panel Cost: £{economics.PanelCost:f0}");
        Console.WriteLine($"Number of panels installed: {economics.PanelCount}");
        Console.WriteLine($"Installation Cost: £{economics.InstallationCost:f0}");
        Console.WriteLine($"Total Initial Outlay: £{economics.TotalInitialOutlay:f0}");

        if (economics.SavedOnBills >= 0)
        {
            Console.WriteLine($"Over {numYears} years the system will save £{economics.SavedOnBills:f0} on electricity bills");
        }
        else
        {
            Console.WriteLine($"Over {numYears} years the system won't generate enough electricity to sell");
        }

        if (economics.GenerationProfit > 0)
        {
            Console.WriteLine($"Over {numYears} years the system will generate £{economics.GenerationProfit:f0} from selling electricity");
        }
        else
        {
            Console.WriteLine($"Over {numYears} years the system won't generate enough electricity to sell");
        }
        Console.WriteLine("========================================================================");
    }

}