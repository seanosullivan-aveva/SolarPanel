using NUnit.Framework;
using SolarPanel.Data;
using SolarPanel.Types;
using System.Linq;

namespace SolarPanel.Application.Tests
{
    [TestFixture]
    internal class CalculationEngine_Tests
    {
        [Test]
        public void Compute_produces_economics()
        {
            // Arrange
            House house = HouseProvider.Instance.Houses.First(o => o.Id == "Seans House");
            Panel panel = PanelProvider.Instance.SolarPanels.First(o => o.Model == "PowerXT® Pure Black™400W");
            Installer installer = InstallerProvider.Instance.Installers.First(o => o.Id == "JNS Solar");
            Tariff tariff = TariffProvider.Instance.Tariffs.First(o => o.Name == "PVA - Standard - Ultra Small - Perpetual");

            // Act
            var economics = CalculationEngine.Compute(house, panel, tariff, installer, 25);

            // Assert
            Assert.That(economics, Is.Not.Null);
            Assert.That(economics.TotalProfit, Is.EqualTo(9794f).Within(1).Percent);
            Assert.That(economics.PanelCount, Is.EqualTo(6));
            Assert.That(economics.GenerationProfit, Is.EqualTo(2370f).Within(1).Percent);
            Assert.That(economics.SavedOnBills, Is.EqualTo(11529f).Within(1).Percent);
            Assert.That(economics.InstallationCost, Is.EqualTo(2100f).Within(1).Percent);

        }

        [TestCase("Seans House", "PowerXT® Pure Black™400W", ExpectedResult = "JNS Solar")]
        [TestCase("Richards Mansion", "PowerXT® Pure Black™400W", ExpectedResult = "Cambridge Solar")]
        public string? Compute_selects_the_best_installer(string houseName, string panelName)
        {
            // Arrange
            House house = HouseProvider.Instance.Houses.First(o => o.Id == houseName);
            Panel panel = PanelProvider.Instance.SolarPanels.First(o => o.Model == panelName);
             
            // Act
            Installer? installer = CalculationEngine.FindCheapestInstaller(house, panel);

            // Assert
            return installer?.Id;
        }

        [TestCase("Seans House", "PowerXT® Pure Black™400W", ExpectedResult = "PVA - Standard - Ultra Small - Perpetual")]
        [TestCase("Richards Mansion", "PowerXT® Pure Black™400W", ExpectedResult = "PVA - Standard - Medium - Perpetual")]
        public string? Compute_finds_the_best_tariff(string houseName, string panelName)
        {
            // Arrange
            House house = HouseProvider.Instance.Houses.First(o => o.Id == houseName);
            Panel panel = PanelProvider.Instance.SolarPanels.First(o => o.Model == panelName);
            Installer installer = InstallerProvider.Instance.Installers.First(o => o.Id == "JNS Solar");

            // Act
            var tariff = CalculationEngine.FindBestTariff(house, panel, installer, 25);

            // Assert
            return tariff?.tariff.Name;
        }

        [TestCase("Seans House", "385W Q Cells ML Mono Q Peak Duo G9+", "PVA - Standard - Ultra Small - Perpetual", "JNS Solar", 25)]
        [TestCase("Richards Mansion", "325W Trina Honey Mono All Black", "PVA - Standard - Medium - Tapered", "Cambridge Solar", 10)]
        public void Compute_finds_the_best_combination(string houseName, string panelName, string tariffName, string installerName, int numYears)
        {
            // Arrange
            House house = HouseProvider.Instance.Houses.First(o => o.Id == houseName);

            // Act
            var result = CalculationEngine.FindBestCombination(house, numYears);

            // Assert
            Assert.That(result?.panel.Model, Is.EqualTo(panelName));
            Assert.That(result?.tariff.Name, Is.EqualTo(tariffName));
            Assert.That(result?.installer.Id, Is.EqualTo(installerName));
        }
    }
}
