using NUnit.Framework;
using SolarPanel.Data;

namespace SolarPanel.Application.Tests;

public class Tariff_Data_Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void data_loading()
    {
        // Arrange

        // Act

        // Assert
        Assert.That(TariffProvider.Instance.Tariffs, Has.Count.GreaterThan(1));
    }
}