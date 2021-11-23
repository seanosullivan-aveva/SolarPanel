﻿using SolarPanel.Data;
using SolarPanel.Logic.Utilities;
using SolarPanel.Types;

namespace SolarPanel.Application
{
    /// <summary>
    /// Provides calculation capabilities for computing solar panel profits
    /// </summary>
    internal class CalculationEngine
    {
        /// <summary>
        /// Computes the economics for the specified configuration
        /// </summary>
        /// <param name="house">The house</param>
        /// <param name="panel">The type of panel fitted</param>
        /// <param name="tariff">The tariff to use</param>
        /// <param name="installer">The installer to use</param>
        /// <param name="numberOfYears">The number of years to compute the economics over</param>
        /// <returns>The economics</returns>
        public static Economics Compute(House house, Panel panel, Tariff tariff, Installer installer, int numberOfYears)
        {
            // Compute the number of panels that can be fitted to the house
            int numberOfPanels = HouseUtilities.MaxNumberOfPanels(house, panel);

            var fitted = new HouseWithPanel(house, panel, numberOfPanels);

            // Compute the purchase price of the panels
            float panelPurchaseCost = numberOfPanels * panel.Cost;
            // Compute the installation cost of the panels
            float installationCost = installer.CallOutCost + (numberOfPanels * installer.CostPerPanel);

            // Compute the total cost of installation and purchase
            float totalCost = panelPurchaseCost + installationCost;

            // Now project into the future looking to see how much electricity is generated by 
            // this house over the coming years
            DateTime now = DateTime.Now;

            float totalMoneyGenerated = 0f;
            float totalMoneySavedOnBills = 0f;
            float totalMoneyMadeInProfit = 0f;

            DateTime? breakEvenDate = null;

            foreach (var day in DateProvider.Instance.GetDays(now, DateProvider.Instance.ComputeYears(now, numberOfYears)))
            {
                // Get the average number of usable daylight hours for today 
                float usableDaylightHours = DaylightProvider.Instance.GetUsableDaylight(day.Month);

                // Get the amount of power generated by the solar panels
                float kiloWatts = HouseUtilities.PowerGeneratedForHouse(fitted, usableDaylightHours) / 1000f;

                // Get the amount consumed by the house for that day (in kilowatts)
                float dailyKiloWattsHouseConsumption = house.DaylightElectricityConsumption / 1000f;

                // Work out how much power is consumed by the house
                float leftOverKiloWatts = kiloWatts - dailyKiloWattsHouseConsumption;

                float kiloWattHoursSentToGrid;
                float kiloWattHoursConsumedByHouse;

                if (leftOverKiloWatts > 0f)
                {
                    // We have some left over to send to the grid
                    kiloWattHoursSentToGrid = leftOverKiloWatts;
                    kiloWattHoursConsumedByHouse = dailyKiloWattsHouseConsumption;
                }
                else
                {
                    // It's all consumed by the house
                    kiloWattHoursSentToGrid = 0;
                    kiloWattHoursConsumedByHouse = kiloWatts;
                }

                // Use the house energy tariff to compute how much was saved from the electricity bill
                float dailySavingOnElectricityBillPounds = house.ElectricityCost * kiloWattHoursConsumedByHouse;

                // Compute the tariff cost
                float dayTariffPencePerKilowattHour = (day <= tariff.Expiry) ? tariff.Price : tariff.ExpiredPrice;
                // Convert the tariff into pounds per kilowatt hour
                float dayTariffPoundPerKilowattHour = dayTariffPencePerKilowattHour / 100f;

                // Use the feed in tariff to compute how much is paid by the electicity supplies
                float dailyProfit = kiloWattHoursSentToGrid * dayTariffPoundPerKilowattHour;

                // Increment the total money for today
                totalMoneyGenerated += dailySavingOnElectricityBillPounds + dailyProfit;
                totalMoneySavedOnBills += dailySavingOnElectricityBillPounds;
                totalMoneyMadeInProfit += dailyProfit;

                if (breakEvenDate.HasValue == false && totalMoneyGenerated > totalCost)
                {
                    // We've broken even!
                    breakEvenDate = day;
                }
            }

            float totalProfitOverTimePeriod = totalMoneyGenerated - totalCost;

            return new Economics(installationCost,
                                    panelPurchaseCost,
                                    numberOfPanels,
                                    totalCost,
                                    breakEvenDate,
                                    breakEvenDate - now,
                                    totalProfitOverTimePeriod,
                                    totalMoneySavedOnBills,
                                    totalMoneyMadeInProfit);
        }

        /// <summary>
        /// Works out who is the cheapest installer
        /// </summary>
        /// <param name="house">The house</param>
        /// <param name="panel">The panel type to be fitted to the house</param>
        /// <returns>The best installer</returns>
        public static Installer? FindCheapestInstaller(House house, Panel panel)
        {
            int numberOfPanels = HouseUtilities.MaxNumberOfPanels(house, panel);

            float bestInstallationCost = float.MaxValue;
            Installer? bestInstaller = null;

            foreach (var installer in InstallerProvider.Instance.Installers)
            {
                // Compute the installation cost of the panels
                float installationCost = installer.CallOutCost + (numberOfPanels * installer.CostPerPanel);

                if(installationCost < bestInstallationCost)
                {
                    bestInstallationCost = installationCost;
                    bestInstaller = installer;
                }
            }

            return bestInstaller;
        }

        /// <summary>
        /// For the specfied house, panel and installer compute the best tariff for this combination
        /// </summary>
        /// <param name="house">The house</param>
        /// <param name="panel">The panel</param>
        /// <param name="installer">The installer of the panels</param>
        /// <param name="numYears">The time frame to consider the tariff over</param>
        /// <returns>The Tariff that produces the greatest financial return over that time span</returns>
        public static (Tariff tariff, Economics economics)? FindBestTariff(House house, Panel panel, Installer installer, int numYears)
        {
            Economics? bestResult = null;
            Tariff? bestTariff = null;

            foreach (var tariff in TariffProvider.Instance.Tariffs)
            {
                var result = Compute(house, panel, tariff, installer, numYears);

                // Check that the tariff is compatible with the house (in kilowatts)               
                var housePower = (result.PanelCount * panel.Power) / 1000f;

                // REFACTOR: Potential performance improvement compute this 
                // before computing the entire economics
                if ((tariff.MinimumFeedAmount.HasValue && housePower < tariff.MinimumFeedAmount)
                    || (tariff.MaximumFeedAmount.HasValue && housePower > tariff.MaximumFeedAmount))
                {
                    // This tariff is unsuitable for this house and configuration
                    continue;
                }

                if(bestResult == null )
                {
                    bestResult = result;
                    bestTariff = tariff;
                }
                else if(result.TotalProfit > bestResult.TotalProfit)
                {
                    bestResult = result;
                    bestTariff = tariff;
                }
            }

            if(bestTariff == null || bestResult == null)
            {
                return null;
            }

            return (bestTariff, bestResult);
        }

        /// <summary>
        /// Find the best combination of tariff, panel, house and installer
        /// </summary>
        /// <param name="house">The house</param>
        /// <param name="numYears">The number of years to consider over</param>
        /// <returns>The combination of tariff, panel, house and installer that produces 
        /// the greatest financial return of the time period</returns>
        public static (Installer installer, Panel panel, Tariff tariff, Economics economics)? FindBestCombination(House house, int numYears)
        {
            Economics? bestResult = null;
            Tariff? bestTariff = null;
            Installer? bestInstaller = null;
            Panel? bestPanel = null;

            foreach (var panel in PanelProvider.Instance.SolarPanels)
            {
                // Find the best installer for this house and panel combination
                var installer = FindCheapestInstaller(house, panel);

                if (installer == null)
                {
                    continue;
                }

                // Now find the best tariff for this house and panel combination
                var tariff = FindBestTariff(house, panel, installer, numYears);

                if (tariff == null)
                {
                    continue;
                }

                // Compare this with the best we've found so far
                if (bestResult == null)
                {
                    bestResult = tariff.Value.economics;
                    bestTariff = tariff.Value.tariff;
                    bestInstaller = installer;
                    bestPanel = panel;
                }
                else if (tariff.Value.economics.TotalProfit > bestResult.TotalProfit)
                {
                    bestResult = tariff.Value.economics;
                    bestTariff = tariff.Value.tariff;
                    bestInstaller = installer;
                    bestPanel = panel;
                }
            }

            if (bestResult == null || bestTariff == null || bestInstaller == null || bestPanel == null)
            {
                return null;
            }

            return (bestInstaller, bestPanel, bestTariff, bestResult);
        }
    }
}
