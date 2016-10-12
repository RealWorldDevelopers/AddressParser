
''' <summary>
''' Entity DTO for Postal Directions (ie N, E, S, W)
''' </summary>
Public Class DirectionalCode
    Inherits BaseCode
End Class

''' <summary>
''' An entity DTO for Terms Commanly Used for Postal Directions
''' </summary>
Public Class DirectionalAlias
    Implements ICodeAlias
    ''' <inheritdoc/>
    Public Property Id As Integer? Implements ICodeAlias.Id
    ''' <inheritdoc/>
    Public Property AliasName As String Implements ICodeAlias.AliasName
    ''' <inheritdoc/>
    Public Property DirectionalCodeId As Integer? Implements ICodeAlias.ForeignId
End Class


