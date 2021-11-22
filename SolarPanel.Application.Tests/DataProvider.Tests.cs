using NUnit.Framework;
using SolarPanel.Data;
using System;
using System.Linq;

namespace SolarPanel.Application.Tests;

public class DateProvider_Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(1.0f, ExpectedResult = 1)]
    [TestCase(10.0f, ExpectedResult = 10)]
    [TestCase(-1.0f, ExpectedResult = 0)]
    [TestCase(365f, ExpectedResult = 365)]
    public int produces_the_right_number_of_days(float days)
    {
        // Act
        return DateProvider.Instance.GetDays(DateTime.Now, TimeSpan.FromDays(days)).Count();
    }

    [Test]
    public void produces_the_right_number_of_days()
    {
        // Arrange
        DateTime january2020 = new DateTime(2020, 1, 1);
        
        // Act
        var twoMonths = DateProvider.Instance.GetDays(january2020, DateProvider.Instance.ComputeMonths(january2020, 2));
        var oneYear = DateProvider.Instance.GetDays(january2020, DateProvider.Instance.ComputeYears(january2020, 1));
        var tenYear = DateProvider.Instance.GetDays(january2020, DateProvider.Instance.ComputeYears(january2020, 10));
        var twentyFiveYear = DateProvider.Instance.GetDays(january2020, DateProvider.Instance.ComputeYears(january2020, 25));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(twoMonths.Count(), Is.EqualTo(60));
            Assert.That(oneYear.Count(), Is.EqualTo(366));
            Assert.That(tenYear.Count(), Is.EqualTo(3653));
            Assert.That(twentyFiveYear.Count(), Is.EqualTo(9132));
        });
     }
}
