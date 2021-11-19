using SolarPanel.Types;
using NUnit.Framework;
using System.Drawing;

namespace SolarPanel.Logic.Tests;

[TestFixture]
public class House_Tests
{
    [SetUp]
    public void Setup()
    {
    }

    private IEnumerable<House> SillyHouse = new List<House>()
    {
        new House("Silly 1", new SizeF(0.0f, 0.0f)),
    };

    [Test]
    public void MaxNumberOfPanels_ignores_silly_data()
    {
        // Arrange

        // Act

        // Assert
    }
}
