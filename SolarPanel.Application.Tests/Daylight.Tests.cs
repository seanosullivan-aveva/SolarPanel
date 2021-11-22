using System;
using System.Collections.Generic;
using NUnit.Framework;
using SolarPanel.Data;

namespace SolarPanel.Application.Tests;

public class DaylightData_Tests
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
        Assert.That(DaylightProvider.Instance.Daylights, Has.Count.EqualTo(12));
    }

    [TestCase(-1)]
    [TestCase(13)]
    public void daylight_throws_if_month_out_of_range(int month)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => DaylightProvider.Instance.GetUsableDaylight(month));
    }

    [TestCaseSource(nameof(ValidMonths))]
    public void daylight_does_not_throw_if_month_is_in_range(int month)
    {
        // Assert
        Assert.DoesNotThrow(() => DaylightProvider.Instance.GetUsableDaylight(month));
    }

    public static IEnumerable<int> ValidMonths = new List<int>() 
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
    };
}
