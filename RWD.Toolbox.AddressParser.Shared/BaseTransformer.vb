''' <summary>
''' Transforms a String Value into an Object
''' </summary>
''' <typeparam name="T">Some Type</typeparam>
Public Interface IDataTransformer(Of T)
    Inherits IDataFormatter(Of T)

    ''' <summary>
    ''' Create Object from String
    ''' </summary>
    ''' <param name="value"><see cref="String"/></param>
    ''' <returns>T</returns>
    Function Parse(value As String) As T
End Interface
