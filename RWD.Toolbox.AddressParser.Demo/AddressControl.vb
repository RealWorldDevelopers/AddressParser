


Imports RWD.Toolbox.AddressParser.Shared


''' <summary>
''' Windows Form User Control for Addresses
''' </summary>
Public Class AddressControl
    Implements IAddressControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim factory As New Factory()
        _addressTransformer = factory.CreateTransformer()
    End Sub

    Private _addressTransformer As IAddressTransformer

    Private _lastTimeKeyHit As Date = Date.Now()

    Public Property KeyToParseDelayInSeconds As Single = 1

#Region "Properties"

    Private _value As New Address
    Public Property Value As Address Implements IAddressControl.Value
        Get
            Return _value
        End Get
        Set(value As Address)
            _value = value
            RefreshDisplayAddress()
        End Set
    End Property

    Public Property FullAddress As String
        Get
            Dim returnValue As String = Nothing
            If Value Is Nothing Then
                returnValue = Nothing
            Else
                If String.IsNullOrWhiteSpace(Value.FullAddress) Then
                    returnValue = _addressTransformer.BuildSingleLine(Value)
                Else
                    returnValue = Value.FullAddress
                End If
            End If
            Return Truncate(returnValue, FullAddressMaxLength)
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.FullAddress = Truncate(val, FullAddressMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressNameDto")
            End If
        End Set
    End Property

    Public ReadOnly Property MultiLine As String
        Get
            Dim returnValue As String = Nothing
            returnValue = _addressTransformer.BuildMultiline(Value)
            Return Truncate(returnValue, MultiLineMaxLength)
        End Get
    End Property

    Public Property Number As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.Number, NumberMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.Number = Truncate(val, NumberMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property PreDirectional As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.PreDirectional, PreDirectionalMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.PreDirectional = Truncate(val, PreDirectionalMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property Street As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.Street, StreetMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.Street = Truncate(val, StreetMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property Suffix As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.Suffix, SuffixMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.Suffix = Truncate(val, SuffixMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property PostDirectional As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.PostDirectional, PostDirectionalMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.PostDirectional = Truncate(val, PostDirectionalMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property SecondaryUnit As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.SecondaryUnit, SecondaryUnitMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.SecondaryUnit = Truncate(val, SecondaryUnitMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property SecondaryNumber As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.SecondaryNumber, SecondaryNumberMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.SecondaryNumber = Truncate(val, SecondaryNumberMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property POBoxNumber As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.POBoxNumber, POBoxNumberMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.POBoxNumber = Truncate(val, POBoxNumberMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property City As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.City, CityMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.City = Truncate(val, CityMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property County As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.County, CountyMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.County = Truncate(val, CountyMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property State As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.State, StateMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.State = Truncate(val, StateMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property ZipPlus4 As String
        Get
            If Value IsNot Nothing Then
                Return Truncate(Value.ZipPlus4, ZipMaxLength)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.ZipPlus4 = Truncate(val, ZipMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property Country As String
        Get
            If Value IsNot Nothing Then
                If Value.Country Is Nothing Then
                    Return "US"
                Else
                    Return Truncate(Value.Country, CountyMaxLength)
                End If

            Else
                Return Nothing
            End If
        End Get
        Set(ByVal val As String)
            If Value IsNot Nothing Then
                Value.Country = Truncate(val, CountyMaxLength)
                RefreshDisplayAddress()
            Else
                Throw New ArgumentNullException("AddressDto")
            End If
        End Set
    End Property

    Public Property FullAddressMaxLength As Integer?
    Public Property MultiLineMaxLength As Integer?
    Public Property StreetLineMaxLength As Integer?
    Public Property NumberMaxLength As Integer?
    Public Property PreDirectionalMaxLength As Integer?
    Public Property StreetMaxLength As Integer?
    Public Property SuffixMaxLength As Integer?
    Public Property PostDirectionalMaxLength As Integer?
    Public Property SecondaryUnitMaxLength As Integer?
    Public Property SecondaryNumberMaxLength As Integer?
    Public Property POBoxNumberMaxLength As Integer?
    Public Property CityMaxLength As Integer?
    Public Property CountyMaxLength As Integer?
    Public Property StateMaxLength As Integer?
    Public Property ZipMaxLength As Integer?
    Public Property CountryMaxLength As Integer?

    Public Property TextFont As Font
        Get
            Return txtAddress.Font
        End Get
        Set(value As Font)
            txtAddress.Font = value
        End Set
    End Property

#End Region

    Public Event AddressSelected(sender As Object, e As EventArgs)
    Public Event ValueChanged As EventHandler Implements IAddressControl.ValueChanged

    Private Sub AddressControl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        With txtAddress
            .Height = ClientRectangle.Height
            .Left = 0
            .Top = 0
            .Width = ClientRectangle.Width
        End With
    End Sub

    Private Sub txtAddress_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtAddress.GotFocus
        txtAddress.SelectAll()
    End Sub

    Private Sub txtAddress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAddress.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged
        _lastTimeKeyHit = Date.Now
        Timer1.Start()
    End Sub

    Private Async Function BuildAddressAsync() As Task
        Value = Await _addressTransformer.ParseAsync(txtAddress.Text)
        Timer1.Stop()
        RaiseEvent ValueChanged(Me, New EventArgs)
    End Function

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Date.Now > DateAdd(DateInterval.Second, KeyToParseDelayInSeconds, _lastTimeKeyHit) Then
            Timer1.Stop()
            Await BuildAddressAsync()
        End If
    End Sub










    Private Function Truncate(val As String, maxLength As Integer?) As String
        Dim result As String = val
        If Not String.IsNullOrEmpty(val) AndAlso maxLength.HasValue Then
            result = If(val.Length <= maxLength.Value, Value, val.Substring(0, maxLength.Value))
        End If
        Return result
    End Function

    Private Sub RefreshDisplayAddress()
        If FullAddress IsNot Nothing Then
            Dim idx As Integer = txtAddress.SelectionStart
            Dim len As Integer = txtAddress.Text.Length
            txtAddress.Text = FullAddress
            If (idx = len) Then
                txtAddress.Select(txtAddress.Text.Length, 0)
            Else
                txtAddress.Select(idx, 0)
            End If
        End If
    End Sub


End Class

Public Interface IAddressControl
    Property Value As Address
    Event ValueChanged As EventHandler
End Interface
