using System.Drawing;
using NUnit.Framework;
using SolarPanel.Types;
using SolarPanel.Logic.Utilities;

namespace SolarPanel.Logic.Tests;

[TestFixture]
public class House_Tests
{
    [SetUp]
    public void Setup()
    {
    }

    static IEnumerable<House> SillyHouses = new List<House>()
    {
        new House("Silly 1", new SizeF(-10.0f, -10.0f), 0,0),
        new House("Silly 2", new SizeF(-10.0f, 10.0f), 0,0),
        new House("Silly 3", new SizeF(10.0f, -10.0f), 0,0),
    };

    [TestCaseSource(nameof(SillyHouses))]
    public void MaxNumberOfPanels_ignores_silly_data(House house)
    {
        // Arrange
        var panel = CreateTestPanel(new SizeF(1, 1));

        // Act

        // Assert
        Assert.That(HouseUtilities.MaxNumberOfPanels(house, panel), Is.EqualTo(0));
    }

    [TestCase(10.0f, 10.0f, 1.0f, 1.0f, ExpectedResult = 100)]
    [TestCase(1.0f, 1.0f, 1.0f, 1.0f, ExpectedResult = 1)]
    [TestCase(1.0f, 1.0f, 2.0f, 1.0f, ExpectedResult = 0)]
    [TestCase(3.0f, 3.0f, 1.1f, 1.1f, ExpectedResult = 4)]
    [TestCase(3.0f, 3.0f, 0.9f, 1.0f, ExpectedResult = 9)]
    [TestCase(3.0f, 3.0f, 1.0f, 1.0f, ExpectedResult = 9)]
    public int MaxNumberOfPanels_computes_right_answer(float rWidth, float rHeight, float pWidth, float pHeight)
    {
        // Arrange
        var panel = CreateTestPanel(new SizeF(pWidth, pHeight));
        var house = CreateTestHouse(new SizeF(rWidth, rHeight));

        // Act

        // Assert
        return HouseUtilities.MaxNumberOfPanels(house, panel);
    }

    private static Panel CreateTestPanel(SizeF size)
    {
        return new Panel(
            "test",
            "test",
            size,
            0.0,
            0.0,
            0.0,
            0.0,
            0.0,
            string.Empty);
    }

    private static House CreateTestHouse(SizeF size)
    {
        return new House("Test", size, 0, 0);
    }
}
