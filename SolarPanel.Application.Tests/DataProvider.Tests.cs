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

}
