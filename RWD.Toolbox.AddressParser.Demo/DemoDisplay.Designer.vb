<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DemoDisplay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Address1 As RWD.Toolbox.AddressParser.[Shared].Address = New RWD.Toolbox.AddressParser.[Shared].Address()
        Me.lblSingleLine = New System.Windows.Forms.Label()
        Me.txtSingleLine = New System.Windows.Forms.TextBox()
        Me.txtDisplay = New System.Windows.Forms.TextBox()
        Me.btnSet = New System.Windows.Forms.Button()
        Me.ctlAddress = New RWD.Toolbox.AddressParser.Demo.AddressControl()
        Me.lblControl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblSingleLine
        '
        Me.lblSingleLine.AutoSize = True
        Me.lblSingleLine.Location = New System.Drawing.Point(55, 16)
        Me.lblSingleLine.Name = "lblSingleLine"
        Me.lblSingleLine.Size = New System.Drawing.Size(97, 13)
        Me.lblSingleLine.TabIndex = 17
        Me.lblSingleLine.Text = "Sample Single Line"
        '
        'txtSingleLine
        '
        Me.txtSingleLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSingleLine.Location = New System.Drawing.Point(58, 32)
        Me.txtSingleLine.Name = "txtSingleLine"
        Me.txtSingleLine.Size = New System.Drawing.Size(377, 26)
        Me.txtSingleLine.TabIndex = 16
        '
        'txtDisplay
        '
        Me.txtDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisplay.Location = New System.Drawing.Point(452, 32)
        Me.txtDisplay.Multiline = True
        Me.txtDisplay.Name = "txtDisplay"
        Me.txtDisplay.Size = New System.Drawing.Size(643, 616)
        Me.txtDisplay.TabIndex = 15
        '
        'btnSet
        '
        Me.btnSet.Location = New System.Drawing.Point(343, 9)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(92, 20)
        Me.btnSet.TabIndex = 14
        Me.btnSet.Text = "Set ControlData"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'ctlAddress
        '
        Me.ctlAddress.City = Nothing
        Me.ctlAddress.CityMaxLength = Nothing
        Me.ctlAddress.Country = "US"
        Me.ctlAddress.CountryMaxLength = Nothing
        Me.ctlAddress.County = Nothing
        Me.ctlAddress.CountyMaxLength = Nothing
        Me.ctlAddress.FullAddress = ""
        Me.ctlAddress.FullAddressMaxLength = Nothing
        Me.ctlAddress.KeyToParseDelayInSeconds = 1.0!
        Me.ctlAddress.Location = New System.Drawing.Point(58, 100)
        Me.ctlAddress.MultiLineMaxLength = Nothing
        Me.ctlAddress.Name = "ctlAddress"
        Me.ctlAddress.Number = Nothing
        Me.ctlAddress.NumberMaxLength = Nothing
        Me.ctlAddress.POBoxNumber = Nothing
        Me.ctlAddress.POBoxNumberMaxLength = Nothing
        Me.ctlAddress.PostDirectional = Nothing
        Me.ctlAddress.PostDirectionalMaxLength = Nothing
        Me.ctlAddress.PreDirectional = Nothing
        Me.ctlAddress.PreDirectionalMaxLength = Nothing
        Me.ctlAddress.SecondaryNumber = Nothing
        Me.ctlAddress.SecondaryNumberMaxLength = Nothing
        Me.ctlAddress.SecondaryUnit = Nothing
        Me.ctlAddress.SecondaryUnitMaxLength = Nothing
        Me.ctlAddress.Size = New System.Drawing.Size(297, 49)
        Me.ctlAddress.State = Nothing
        Me.ctlAddress.StateMaxLength = Nothing
        Me.ctlAddress.Street = Nothing
        Me.ctlAddress.StreetLineMaxLength = Nothing
        Me.ctlAddress.StreetMaxLength = Nothing
        Me.ctlAddress.Suffix = Nothing
        Me.ctlAddress.SuffixMaxLength = Nothing
        Me.ctlAddress.TabIndex = 18
        Me.ctlAddress.TextFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Address1.City = Nothing
        Address1.Country = Nothing
        Address1.County = Nothing
        Address1.FullAddress = Nothing
        Address1.Number = Nothing
        Address1.POBoxNumber = Nothing
        Address1.PostDirectional = Nothing
        Address1.PreDirectional = Nothing
        Address1.SecondaryNumber = Nothing
        Address1.SecondaryUnit = Nothing
        Address1.State = Nothing
        Address1.Street = Nothing
        Address1.Suffix = Nothing
        Address1.ZipPlus4 = Nothing
        Me.ctlAddress.Value = Address1
        Me.ctlAddress.ZipMaxLength = Nothing
        Me.ctlAddress.ZipPlus4 = Nothing
        '
        'lblControl
        '
        Me.lblControl.AutoSize = True
        Me.lblControl.Location = New System.Drawing.Point(55, 84)
        Me.lblControl.Name = "lblControl"
        Me.lblControl.Size = New System.Drawing.Size(119, 13)
        Me.lblControl.TabIndex = 19
        Me.lblControl.Text = "Sample Address Control"
        '
        'DemoDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1460, 657)
        Me.Controls.Add(Me.lblControl)
        Me.Controls.Add(Me.ctlAddress)
        Me.Controls.Add(Me.lblSingleLine)
        Me.Controls.Add(Me.txtSingleLine)
        Me.Controls.Add(Me.txtDisplay)
        Me.Controls.Add(Me.btnSet)
        Me.Name = "DemoDisplay"
        Me.Text = "DemoDisplay"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSingleLine As Label
    Friend WithEvents txtSingleLine As TextBox
    Friend WithEvents txtDisplay As TextBox
    Friend WithEvents btnSet As Button
    Friend WithEvents ctlAddress As AddressControl
    Friend WithEvents lblControl As Label
End Class
