using NUnit.Framework;
using SolarPanel.Data;

namespace SolarPanel.Application.Tests;

public class SolarPanelData_Tests
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
        Assert.That(DataProvider.Instance.SolarPanels, Has.Count.GreaterThan(1));
    }
}