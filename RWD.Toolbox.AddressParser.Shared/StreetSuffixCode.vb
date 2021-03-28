''' <summary>
''' Entity DTO for Postal Street Suffixes (ie St, Ave, Rd)
''' </summary>
Public Class StreetSuffixCode
    Inherits BaseCode
End Class

''' <summary>
''' An entity DTO for Terms Commonly Used for Postal Street Suffixes
''' </summary>
Public Class StreetSuffixAlias
    Implements ICodeAlias
    ''' <inheritdoc/>
    Public Property Id As Integer? Implements ICodeAlias.Id
    ''' <inheritdoc/>
    Public Property AliasName As String Implements ICodeAlias.AliasName
    ''' <inheritdoc/>
    Public Property StreetSuffixCodeId As Integer? Implements ICodeAlias.ForeignId
End Class
