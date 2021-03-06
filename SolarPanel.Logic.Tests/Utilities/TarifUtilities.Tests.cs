using System.Drawing;
using NUnit.Framework;
using SolarPanel.Types;
using SolarPanel.Logic.Utilities;
using static TestUtilities;
using System;

namespace SolarPanel.Logic.Tests;

[TestFixture]
public class TarifUtilities_Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Computes_generation_cost_correctly_simple()
    {
        // Arrange
        Tariff tariff = CreateTariff(1.0f, DateTime.MaxValue, 1.0f);

        double amountGenerated = 100;
        DateTime when = new DateTime(2021, 11, 19);
        TimeSpan over = TimeSpan.FromDays(1.0);

        // Act
        double amountPaid = TariffUtilities.ComputeAmountPaid(tariff, amountGenerated, when, over);

        // Assert
        Assert.That(amountPaid, Is.EqualTo(0.024));
    }

    [Test]
    public void Computes_generation_cost_correctly_simple_2()
    {
        // Arrange
        Tariff tariff = CreateTariff(1.0f, DateTime.MaxValue, 1.0f);

        double amountGenerated = 320;
        DateTime when = new DateTime(2021, 11, 19);
        TimeSpan over = TimeSpan.FromHours(8.0);

        // Act
        double amountPaid = TariffUtilities.ComputeAmountPaid(tariff, amountGenerated, when, over);

        // Assert
        Assert.That(amountPaid, Is.EqualTo(0.0256));
    }

    [Test]
    public void Computes_generation_cost_correctly_crosses_expiry_date()
    {
        // Arrange
        Tariff tariff = CreateTariff(1.0f, DateTime.Now, 0.5f);

        double amountGenerated = 320;
        DateTime when = DateTime.Now + TimeSpan.FromDays(1);
        TimeSpan over = TimeSpan.FromHours(8.0);

        // Act
        double amountPaid = TariffUtilities.ComputeAmountPaid(tariff, amountGenerated, when, over);

        // Assert
        Assert.That(amountPaid, Is.EqualTo(0.0128));
    }

    [Test]
    public void Computes_generation_cost_correctly_complex()
    {
        // Arrange
        Tariff tariff = CreateTariff(4.42f, DateTime.MaxValue, 1.32f);

        double amountGenerated = 320;
        DateTime when = DateTime.Now + TimeSpan.FromDays(1);
        TimeSpan over = TimeSpan.FromHours(8.0);

        // Act
        double amountPaid = TariffUtilities.ComputeAmountPaid(tariff, amountGenerated, when, over);

        // Assert
        Assert.That(amountPaid, Is.EqualTo(0.113152).Within(1).Percent);
    }

}
