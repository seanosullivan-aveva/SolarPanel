Imports NUnit.Framework
Imports SolarPanel.Types

<TestFixture>
Public Class TariffUtilities_Tests

    <TestCase(1.0F, 1.0F, 8.0F, ExpectedResult:=0.00008)>
    Function TariffUtilities_Panels(price, power, hours)
        ' Arrange
        Dim tariff = New Tariff("test", price, DateTime.MaxValue, 0F, vbNull, vbNull)

        ' Act
        Dim result = TariffUtilities.ComputeAmountPaid(tariff, power, New Date(2021, 1, 1), TimeSpan.FromHours(hours))

        ' Assert
        Return result
    End Function
End Class
