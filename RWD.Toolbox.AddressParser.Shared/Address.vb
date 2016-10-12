''' <summary>
''' IO DTO Used by Address Control
''' </summary>
''' <remarks></remarks>
Public Class Address

    ''' <summary>
    ''' A single line representation of the entire address
    ''' </summary>
    Public Property FullAddress As String

    ''' <summary>
    ''' The house number.
    ''' </summary>
    Public Property Number As String

    ''' <summary>
    ''' The predirectional, such as "N" in "500 N Main St".
    ''' </summary>
    Public Property PreDirectional As String

    ''' <summary>
    ''' The name of the street, such as "Main" in "500 N Main St".
    ''' </summary>
    Public Property Street As String

    ''' <summary>
    ''' The street suffix, such as "ST" in "500 N MAIN ST".
    ''' </summary>
    Public Property Suffix As String

    ''' <summary>
    ''' The postdirectional, such as "NW" in "500 Main St NW".
    ''' </summary>
    Public Property PostDirectional As String

    ''' <summary>
    ''' The secondary unit, such as "APT" in "500 N MAIN ST APT 3".
    ''' </summary>
    Public Property SecondaryUnit As String

    ''' <summary>
    ''' The secondary unit number, such as "3" in "500 N MAIN ST APT 3".
    ''' </summary>
    Public Property SecondaryNumber As String

    ''' <summary>
    ''' Post Office Box Number
    ''' </summary>
    Public Property POBoxNumber As String

    ''' <summary>
    ''' The city name.
    ''' </summary>
    Public Property City As String

    ''' <summary>
    ''' The County Code
    ''' </summary>
    Public Property County As String

    ''' <summary>
    ''' The state or territory.
    ''' </summary>
    Public Property State As String

    ''' <summary>
    ''' The ZIP code.
    ''' </summary>
    Public Property ZipPlus4 As String

    ''' <summary>
    ''' The Country Code.
    ''' </summary>
    Public Property Country As String

End Class
