using System;
using CommandLine;
using SolarPanel.Data;

public class Program
{
    public class Options
    {
        [Option('a', "action", Required = false, HelpText = "The action to perform")]
        public string Action { get; set; }
    }

    static void Main(string[] args)
    {
        //Console.WriteLine("Starting run");

        //var houses = HouseProvider.Instance.Houses;
        //var tariffs = TariffProvider.Instance.Tariffs;
        //var panels = PanelProvider.Instance.SolarPanels;

        //// Insert Solution Here



        //Console.WriteLine("Write Answer Here");

       

        // Implementation.ReadData();
        //Implementation.KnownHouseAndPanel();
        // Implementation.FindMeBestTariff();
        Implementation.FindMeBestCombinationOfPanelAndTariff();

        //Parser.Default.ParseArguments<Options>(args)
        //       .WithParsed<Options>(o =>
        //       {
        //           if (o.Verbose)
        //           {
        //               Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
        //               Console.WriteLine("Quick Start Example! App is in Verbose mode!");
        //           }
        //           else
        //           {
        //               Console.WriteLine($"Current Arguments: -v {o.Verbose}");
        //               Console.WriteLine("Quick Start Example!");
        //           }
        //       });
    }
}

