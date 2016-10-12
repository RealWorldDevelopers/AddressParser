Public Class DemoDisplay

    Private Sub DemoDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ctlAddress.TextFont = txtSingleLine.Font
        txtSingleLine.Text = "7768 s anderson ave ne apartment   2   PO Box 99 warren  ohio 44484-9987 usa"
        _resize()

    End Sub

    Private Sub _resize()
        Dim margin As Integer = 5

        txtSingleLine.Width = CInt((ClientRectangle.Width / 2) - margin * 3)
        txtDisplay.Width = txtSingleLine.Width

        btnSet.Top = margin * 2
        lblSingleLine.Top = btnSet.Bottom - lblSingleLine.Height
        txtSingleLine.Top = lblSingleLine.Bottom + margin
        txtDisplay.Top = txtSingleLine.Top


        txtSingleLine.Left = lblSingleLine.Left
        txtDisplay.Left = txtSingleLine.Right + margin
        btnSet.Left = txtSingleLine.Right - btnSet.Width

        lblControl.Top = txtSingleLine.Bottom + margin
        lblControl.Left = txtSingleLine.Left
        If ctlAddress IsNot Nothing Then
            ctlAddress.Width = txtSingleLine.Width
            ctlAddress.Top = lblControl.Bottom
            ctlAddress.Left = txtSingleLine.Left
        End If
    End Sub

    Private Sub DemoDisplay_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        _resize()
    End Sub

    Private Sub ctlAddress_ValueChanged() Handles ctlAddress.ValueChanged
        UpdateParseResultDisplay()
    End Sub

    Private Sub btnSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSet.Click
        ctlAddress.FullAddress = txtSingleLine.Text
        UpdateParseResultDisplay()
    End Sub

    Public Sub UpdateParseResultDisplay()
        Try

            'display block
            txtDisplay.Text = "FullAddress: " & ctlAddress.FullAddress & vbNewLine & vbNewLine

            'txtDisplay.AppendText("StreetLine: " & Me.ctlAddress.StreetLine & vbNewLine)
            txtDisplay.AppendText("Number: " & ctlAddress.Number & vbNewLine)
            txtDisplay.AppendText("Predirectional: " & ctlAddress.PreDirectional & vbNewLine)
            txtDisplay.AppendText("Street: " & ctlAddress.Street & vbNewLine)
            txtDisplay.AppendText("Suffix: " & ctlAddress.Suffix & vbNewLine)
            txtDisplay.AppendText("Postdirectional: " & ctlAddress.PostDirectional & vbNewLine)
            txtDisplay.AppendText("SecondaryUnit: " & ctlAddress.SecondaryUnit & vbNewLine)
            txtDisplay.AppendText("SecondaryNumber: " & ctlAddress.SecondaryNumber & vbNewLine & vbNewLine)

            txtDisplay.AppendText("PO Box: " & ctlAddress.POBoxNumber & vbNewLine)

            txtDisplay.AppendText("City: " & ctlAddress.City & vbNewLine)
            txtDisplay.AppendText("County: " & ctlAddress.County & vbNewLine)
            txtDisplay.AppendText("State: " & ctlAddress.State & vbNewLine)
            txtDisplay.AppendText("ZipPlus4: " & ctlAddress.ZipPlus4 & vbNewLine)
            txtDisplay.AppendText("Country: " & ctlAddress.Country & vbNewLine & vbNewLine)

            txtDisplay.AppendText("MultiLine: " & vbNewLine & ctlAddress.MultiLine)

        Catch ex As Exception
            Stop
        End Try

    End Sub


End Class