
''' <summary>
''' A base class which can be used by most typical entity DTOs for code-tables
''' </summary>
''' <remarks>
''' DO NOT assume that code entity DTOs inherit from this class.  If this 
''' implementation is insufficient for a particular class of DTO, that class 
''' should not inherit from this base class.  Rather, it should implement the 
''' <see cref="ICode"/> interface directly, providing its own custom 
''' implementation.
''' </remarks>
Public MustInherit Class BaseCode
    Implements ICode

    ''' <inheritdoc/>
    Public Property Id As Integer? Implements ICode.Id

    ''' <inheritdoc/>
    Public Property Code As String Implements ICode.Code

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
        If obj.GetType() = Me.GetType() Then
            Dim other As BaseCode = DirectCast(obj, BaseCode)
            If ((other IsNot Nothing) AndAlso Id.HasValue AndAlso other.Id.HasValue AndAlso (other.Id.Value = Id.Value)) Then
                result = True
            End If
        End If
        Return result
    End Function
End Class


''' <summary>
''' An entity DTO for an RMS code-table
''' </summary>
''' <remarks>
''' All classes implementing this type must also override the base Object.Equals method.
''' </remarks>
Public Interface ICode

    ''' <summary>
    ''' The hidden unique ID for the code
    ''' </summary>
    Property Id As Integer?

    ''' <summary>
    ''' The short but meaningful abbreviation for the code which may be displayed to 
    ''' or entered by users
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' The full name or long description of the code which may be displayed to users
    ''' </summary>
    ''' <remarks>
    ''' This has also been referred to as the code's "literal".
    ''' </remarks>
    Property Description As String
End Interface


''' <summary>
''' An entity DTO for Terms Commanly Used for Codes
''' </summary>
Public Interface ICodeAlias

    ''' <summary>
    ''' The unique ID for the alias
    ''' </summary>
    Property Id As Integer?

    ''' <summary>
    ''' A commonly used abreviation that is not an actual code 
    ''' </summary>
    Property AliasName As String

    ''' <summary>
    ''' Id of the Related Table
    ''' </summary>
    Property ForeignId As Integer?

End Interface


