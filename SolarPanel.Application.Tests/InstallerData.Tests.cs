using NUnit.Framework;
using SolarPanel.Data;

namespace SolarPanel.Application.Tests;

public class InstallerData_Tests
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
        Assert.That(InstallerProvider.Instance.Installers, Has.Count.GreaterThan(1));
    }
}
