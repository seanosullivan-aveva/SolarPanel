using NUnit.Framework;
using SolarPanel.House.Data;

namespace SolarPanel.Application.Tests;

public class HouseData_Tests
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
        Assert.That(HouseProvider.Instance.Houses, Has.Count.GreaterThan(1));
    }
}
