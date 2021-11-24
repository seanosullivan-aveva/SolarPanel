Imports SolarPanel.Data

Module Program
    Sub Main(args As String())
        Dim Houses = HouseProvider.Instance.Houses
        Dim Tariffs = TariffProvider.Instance.Tariffs
        Dim Installers = InstallerProvider.Instance.Installers
        Dim SolarPanels = PanelProvider.Instance.SolarPanels

        Console.WriteLine("Hello World!")
    End Sub
End Module
