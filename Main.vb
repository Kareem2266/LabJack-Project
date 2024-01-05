
Imports LabJack.LabJackUD
'Imports System.Math
Public Class frmDAQ

    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        End
    End Sub

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        lblVoltage1Explination.Visible = True
        lblVoltage2Explination.Visible = True
        lblVoltageResistance.Visible = True
        lblCurrent.Visible = True
        lblResistor.Visible = True
        lblVoltage1.Visible = True
        lblVoltage2.Visible = True
        lblVResistance.Visible = True
        lblCurrentV.Visible = True
        lblResistorV.Visible = True



        Dim ioType As LJUD.IO
        Dim channel As LJUD.CHANNEL
        Dim dblVal As Double
        Dim intVal As Integer
        Dim val As Double
        Dim ValueAIN(16) As Double
        Dim numIterations As Long
        Dim numChannels As Integer
        Dim u3 As U3

        numIterations = 100
        numChannels = 2 'Number of AIN channels, 0-16.

        u3 = New U3(LJUD.CONNECTION.USB, "0", True) ' Connection through USB

        'Add analog input requests.
        For j As Integer = 0 To numChannels - 1
            LJUD.AddRequest(u3.ljhandle, LJUD.IO.GET_AIN, j, 0, 0, 0)
        Next j

        For i As Integer = 0 To numIterations - 1

            'Execute the requests.
            LJUD.GoOne(u3.ljhandle)

            'Get all the results.  The input measurement results are stored.  All other
            'results are for configuration or output requests so we are just checking
            'whether there was an error.
            LJUD.GetFirstResult(u3.ljhandle, ioType, channel, val, intVal, dblVal)

            ' Get results until there is no more data available
            Dim isFinished As Boolean
            isFinished = False
            While Not isFinished
                ValueAIN(channel) = val

                Try
                    LJUD.GetNextResult(u3.ljhandle, ioType, channel, val, intVal, dblVal)
                Catch ex As LabJackUDException
                    If ex.LJUDError = UE9.LJUDERROR.NO_MORE_DATA_AVAILABLE Then
                        isFinished = True
                    End If
                End Try
            End While
        Next i

        ' lblVoltage1.Text = ValueAIN(0)
        'lblVoltage2.Text = ValueAIN(1)
        lblVoltage1.Text = Math.Round(ValueAIN(1), 2) & " Volts"
        lblVoltage2.Text = Math.Round(ValueAIN(0), 2) & " Volts"
        lblVResistance.Text = Math.Round(Math.Round(ValueAIN(1), 2) - Math.Round(ValueAIN(0), 2), 2) & " Volts"
        lblCurrentV.Text = Math.Round(ValueAIN(0), 2) * 10 & " mA"
        lblResistorV.Text = Math.Round((Math.Round(ValueAIN(1), 2) - Math.Round(ValueAIN(0), 2)) / (Math.Round(ValueAIN(0), 2) * 10), 2) & " kΩ"


    End Sub

End Class
