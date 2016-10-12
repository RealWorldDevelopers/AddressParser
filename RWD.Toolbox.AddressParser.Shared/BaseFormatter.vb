''' <summary>
''' Reads an Object and Returns the Proper String Value
''' </summary>
''' <typeparam name="T">Some Type</typeparam>
Public Interface IDataFormatter(Of T)
    ''' <summary>
    ''' Get Formated String Value
    ''' </summary>
    ''' <param name="value">Object to Parse</param>
    ''' <returns>Formated <see cref="String"/></returns>
    Function Format(value As T) As String
End Interface
