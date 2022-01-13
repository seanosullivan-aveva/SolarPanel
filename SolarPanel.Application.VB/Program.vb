Imports SolarPanel.Data

Module Program
    Sub Main(args As String())
        If args.Length = 0 Then
            Console.WriteLine("Please provide the name of the house")
            Return
        End If

        Implementation.FindMeBestCombinationOfPanelAndTariff(args.ElementAt(0))
    End Sub
End Module
