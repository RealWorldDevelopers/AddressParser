''' <summary>
''' Entity DTO for Postal State (ie OH, FL, PA)
''' </summary>
Public Class StateCode
    Implements ICode

    '''<inheritdoc/>
    Property StateCode As String Implements ICode.Code

    ''' <summary>
    ''' Country where County Resides
    ''' </summary>
    ''' <returns>Country where County Resides as <see cref="CountryCode"/></returns>
    Public Property CountryCodeId As Integer?

    '''<inheritdoc/>
    Public Property Id As Integer? Implements ICode.Id

    '''<inheritdoc/>
    Public Property Description As String Implements ICode.Description

    ''' <summary>
    ''' Compare two Code Objects by Id
    ''' </summary>
    ''' <param name="obj">Object to Compare as <see cref="Object"/></param>
    ''' <returns>Compare Results as <see cref="Boolean"/></returns>
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim result As Boolean = False
        If TypeOf obj Is StateCode Then
            Dim other As StateCode = DirectCast(obj, StateCode)
            If ((other IsNot Nothing) AndAlso Id.HasValue AndAlso other.Id.HasValue AndAlso (other.Id.Value = Id.Value)) Then
                result = True
            End If
        End If
        Return result
    End Function

End Class


''' <summary>
''' An entity DTO for Terms Commonly Used for Postal States
''' </summary>
Public Class StateAlias
    Implements ICodeAlias

    '''<inheritdoc/>
    Public Property Id As Integer? Implements ICodeAlias.Id

    '''<inheritdoc/>
    Public Property AliasName As String Implements ICodeAlias.AliasName

    ''' <summary>
    ''' The State Code Id Alias is Related to
    ''' </summary>
    ''' <returns>StateCode as <see cref="Integer?"/></returns>
    Public Property StateCodeId As Integer?

    '''<inheritdoc/>
    Public Property CountryCodeId As Integer? Implements ICodeAlias.ForeignId

End Class
