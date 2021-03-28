''' <summary>
''' Entity DTO for Postal Secondary Units (ie Apt)
''' </summary>
Public Class StreetSecondaryUnitCode
    Inherits BaseCode
    ''' <summary>
    ''' Does Unit Code expect a Range as a Value
    ''' </summary>
    ''' <returns>Unit Code expect a range as a value as <see cref="Boolean"/></returns>
    Public Property RequiresRange As Boolean
End Class

''' <summary>
''' An entity DTO for Terms Commonly Used for Postal Secondary Units
''' </summary>
Public Class StreetSecondaryUnitAlias
    Implements ICodeAlias
    ''' <inheritdoc/>
    Public Property Id As Integer? Implements ICodeAlias.Id
    ''' <inheritdoc/>
    Public Property AliasName As String Implements ICodeAlias.AliasName
    ''' <inheritdoc/>
    Public Property ForeignId As Integer? Implements ICodeAlias.ForeignId
End Class