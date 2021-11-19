using System.Drawing;
using NUnit.Framework;
using SolarPanel.Types;
using SolarPanel.Logic.Utilities;
using static TestUtilities;

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

    [TestCase(0, 0, ExpectedResult = 0f)]
    [TestCase(1, 0, ExpectedResult = 0f)]
    [TestCase(2, 0, ExpectedResult = 0f)]
    [TestCase(10, 0, ExpectedResult = 0f)]
    [TestCase(100, 0, ExpectedResult = 0f)]
    [TestCase(0, 1, ExpectedResult = 0f)]
    [TestCase(1, 1, ExpectedResult = 100f)]
    [TestCase(2, 1, ExpectedResult = 200f)]
    [TestCase(10, 1, ExpectedResult = 1000f)]
    [TestCase(100, 1, ExpectedResult = 10000f)]
    [TestCase(0, 2, ExpectedResult = 0f)]
    [TestCase(1, 2, ExpectedResult = 200f)]
    [TestCase(2, 2, ExpectedResult = 400f)]
    [TestCase(10, 2, ExpectedResult = 2000f)]
    [TestCase(100, 2, ExpectedResult = 20000f)]
    public float PowerGeneratedForHouse_generates_correct_results(int numberOfPanels, int daylightHours)
    {
        // Arrange
        var house = CreateTestHouse(new SizeF(5, 3));
        var panel = CreateTestPanel(new SizeF(1, 1), power:100.0f);
        var housePanel = new HouseWithPanel(house, panel, numberOfPanels);

        // Act
        return HouseUtilities.PowerGeneratedForHouse(housePanel, daylightHours);
    }


}
