Imports RWD.Toolbox.AddressParser.Shared
''' <summary>
''' Class for Providing Clean New Objects
''' </summary>
Public Class Factory

    ''' <summary>
    ''' Set of Codes Needed for Address Control to Function
    ''' </summary>
    Public MasterCodeSet As IMasterCodeSet

    Public Sub New()
        MasterCodeSet = New MasterCodeSet()
    End Sub

    ''' <summary>
    ''' Retrieve a Address Transformer Object
    ''' </summary>
    ''' <returns>Address Transformer Object as <see cref="IAddressTransformer"/></returns>
    Function CreateTransformer() As IAddressTransformer
        Return New AddressTransformer(CreateMasterCodeSet)
    End Function

    ''' <summary>
    ''' Retrieve a Set of Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>Code Set as <see cref="IMasterCodeSet"/></returns>
    Public Function CreateMasterCodeSet() As IMasterCodeSet
        Return MasterCodeSet
    End Function

    ''' <summary>
    ''' List of Direction Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of DirectionalCode)"/></returns>
    Public Function CreateDirectionalCodes() As List(Of DirectionalCode)
        Return MasterCodeSet.DirectionalCodes
    End Function

    ''' <summary>
    ''' List of Directional Aliases Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of DirectionalAlias)"/></returns>
    Public Function CreateDirectionalAliases() As List(Of DirectionalAlias)
        Return MasterCodeSet.DirectionalAliases
    End Function

    ''' <summary>
    ''' List of Street Suffix Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of StreetSuffixCode)"/></returns>
    Public Function CreateStreetSuffixCodes() As List(Of StreetSuffixCode)
        Return MasterCodeSet.StreetSuffixCodes
    End Function

    ''' <summary>
    ''' List of Street Suffix Aliases Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of StreetSuffixAlias)"/></returns>
    Public Function CreateStreetSuffixAliases() As List(Of StreetSuffixAlias)
        Return MasterCodeSet.StreetSuffixAliases
    End Function

    ''' <summary>
    ''' List of Street Secondary Unit Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of StreetSecondaryUnitCode)"/></returns>
    Public Function CreateStreetSecondaryUnitCodes() As List(Of StreetSecondaryUnitCode)
        Return MasterCodeSet.StreetSecondaryUnitCodes
    End Function

    ''' <summary>
    ''' List of Street Secondary Unit Alias Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of StreetSecondaryUnitAlias)"/></returns>
    Public Function CreateStreetSecondaryUnitAliases() As List(Of StreetSecondaryUnitAlias)
        Return MasterCodeSet.StreetSecondaryUnitAliases
    End Function

    ''' <summary>
    ''' List of County Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of CountyCode)"/></returns>
    Public Function CreateCountyCodes() As List(Of CountyCode)
        Return MasterCodeSet.CountyCodes
    End Function

    ''' <summary>
    ''' List of State Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of StateCode)"/></returns>
    Public Function CreateStateCodes() As List(Of StateCode)
        Return MasterCodeSet.StateCodes
    End Function

    ''' <summary>
    ''' List of State Alias Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of StateAlias)"/></returns>
    Public Function CreateStateAliases() As List(Of StateAlias)
        Return MasterCodeSet.StateAliases
    End Function

    ''' <summary>
    ''' List of Country Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of CountryCode)"/></returns>
    Public Function CreateCountryCodes() As List(Of CountryCode)
        Return MasterCodeSet.CountryCodes
    End Function

    ''' <summary>
    ''' List of Country Alias Codes Needed for Address Control to Function
    ''' </summary>
    ''' <returns>List of Codes as <see cref="List(Of CountryAlias)"/></returns>
    Public Function CreateCountryAliases() As List(Of CountryAlias)
        Return MasterCodeSet.CountryAliases
    End Function

   ''' <summary>
   ''' List of Zip Code Codes Needed for Address Control to Function
   ''' </summary>
   ''' <returns>List of Codes as <see cref="List(Of ZipcodeCode)"/></returns>
   Public Function CreateZipcodeCodes() As List(Of ZipcodeCode)
        Return MasterCodeSet.ZipcodeCodes
    End Function

End Class
