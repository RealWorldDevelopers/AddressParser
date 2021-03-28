''' <summary>
''' Entity DTO for Postal Country (ie CA, USA)
''' </summary>
Public Class CountryCode
    Inherits BaseCode
End Class


''' <summary>
''' An entity DTO for Terms Commonly Used for Postal Countries
''' </summary>
Public Class CountryAlias
    Implements ICodeAlias
    ''' <inheritdoc/>
    Public Property Id As Integer? Implements ICodeAlias.Id
    ''' <inheritdoc/>
    Public Property AliasName As String Implements ICodeAlias.AliasName
    ''' <inheritdoc/>
    Public Property CountryCodeId As Integer? Implements ICodeAlias.ForeignId
End Class