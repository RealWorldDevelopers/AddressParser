

''' <summary>
''' Entity DTO for County Field
''' </summary>
Public Class CountyCode
    Implements ICode

    ''' <inheritdoc/>
    Property CountyCode As String Implements ICode.Code

    ''' <summary>
    ''' State where County Resides
    ''' </summary>
    ''' <returns>Country where County Resides as <see cref="StateCode"/></returns>
    Property StateCode As String

    ''' <summary>
    ''' Country where County Resides
    ''' </summary>
    ''' <returns>Country where County Resides as <see cref="CountryCode"/></returns>
    Property CountryCode As String

    ''' <inheritdoc/>
    Public Property Id As Integer? Implements ICode.Id

    ''' <inheritdoc/>
    Public Property Description As String Implements ICode.Description

    ''' <summary>
    ''' Returns the Description Property as the ToString Method
    ''' </summary>
    ''' <returns><see cref="String"/></returns>
    Public Overrides Function ToString() As String
        Return Description
    End Function

    ''' <summary>
    ''' Compare two Code Objects by Id
    ''' </summary>
    ''' <param name="obj">Object to Compare as <see cref="Object"/></param>
    ''' <returns>Compare Results as <see cref="Boolean"/></returns>
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim result As Boolean = False
        If TypeOf obj Is CountyCode Then
            Dim other As CountyCode = DirectCast(obj, CountyCode)
            If ((other IsNot Nothing) AndAlso Id.HasValue AndAlso other.Id.HasValue AndAlso (other.Id.Value = Id.Value)) Then
                result = True
            End If
        End If
        Return result
    End Function

End Class
