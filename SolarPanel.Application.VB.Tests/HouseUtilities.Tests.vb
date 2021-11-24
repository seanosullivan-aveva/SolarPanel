Imports System
Imports System.Drawing
Imports NUnit.Framework
Imports SolarPanel.Logic.Utilities
Imports SolarPanel.Types

<TestFixture>
Public Class HouseUtilities_Tests

    <TestCase(4.0F, 2.0F, 1.0F, 1.0F, ExpectedResult:=8)>
    <TestCase(4.0F, 2.0F, 1.1F, 1.1F, ExpectedResult:=3)>
    <TestCase(4.0F, 2.0F, 0.5F, 0.5F, ExpectedResult:=32)>
    Function HouseUtilities_Panels(roofWidth, roofHeight, panelHeight, panelWidth)
        ' Arrange
        Dim house = New House("test", New SizeF(roofWidth, roofHeight), 0F, 0F)
        Dim panel = New Panel("", "test", New SizeF(panelHeight, panelWidth), 0F, 0F, 0F, 0F, String.Empty)

        ' Act
        Dim result = HouseUtilities.MaxNumberOfPanels(house, panel)

        ' Assert
        Return result
    End Function
End Class
