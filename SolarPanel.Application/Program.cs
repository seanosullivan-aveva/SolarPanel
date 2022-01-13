using System;
using CommandLine;
using SolarPanel.Data;

public class Program
{
    static void Main(string[] args)
    {
        if(args.Length == 0)
        {
            Console.WriteLine("Please provide the name of the house");
            return;
        }

        Implementation.FindMeBestCombinationOfPanelAndTariff(args[0]);
    }
}

