Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports RWD.Toolbox.AddressParser.Shared


''' <summary>
''' Main Class for an Object to Transforms Addresses
''' </summary>
Public Class AddressTransformer
    Implements IAddressTransformer

    Private Class GroupNames
        Public Const Number As String = "Number"
        Public Const PreDirectional As String = "PreDirectional"
        Public Const Street As String = "Street"
        Public Const Suffix As String = "Suffix"
        Public Const PostDirectional As String = "PostDirectional"
        Public Const SecondaryUnit As String = "SecondaryUnit"
        Public Const SecondaryNumber As String = "SecondaryNumber"
        Public Const POBoxNumber As String = "POBoxNumber"
        Public Const City As String = "City"
        Public Const County As String = "County"
        Public Const State As String = "State"
        Public Const ZipPlus4 As String = "ZipPlus4"
        Public Const Country As String = "Country"
    End Class

    ''' <summary>
    ''' Initializes the <see cref="AddressTransformer"/> class.
    ''' </summary>
    ''' <param name="masterCodeSet">Set of Codes Needed for Address Control to Function as <see cref="MasterCodeSet"/></param>
    Public Sub New(masterCodeSet As IMasterCodeSet)
        _masterCodeSet = masterCodeSet
        If _masterCodeSet IsNot Nothing Then
            RegExStartUp = Task.Run(Sub() _addressRegex_Compiled = BuildCompiledRegex())
        End If
    End Sub

    Dim _addressDtoTemplate As New Address
    Private _masterCodeSet As IMasterCodeSet
    Private RegExStartUp As Task
    Private _addressRegex_Compiled As Regex

#Region "For Testing Only"

    Private _useReducedPatternForTesting As Boolean = False
    Private _displayPatternsForTesting As Boolean = False

    Private Sub ShowStringInTextEditor(pattern As String, title As String)
        Dim tempFile As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, title & ".txt")

        File.WriteAllText(tempFile, tempFile & vbNewLine & vbNewLine)
        File.AppendAllText(tempFile, pattern)

        Process.Start(tempFile)
    End Sub

#End Region


    ''' <inheritdoc/>
    Public Function Format(value As Address) As String Implements IAddressTransformer.Format
        If value IsNot Nothing Then
            Return BuildSingleLine(value)
        Else
            Return Nothing
        End If
    End Function

    ''' <inheritdoc/>
    Public Function Parse(value As String) As Address Implements IAddressTransformer.Parse
        Return ParseAddress(value)
    End Function

    ''' <inheritdoc/>
    Public Async Function ParseAsync(value As String) As Task(Of Address) Implements IAddressTransformer.ParseAsync
        Return Await Task.Run(Function() ParseAddress(value))
    End Function

    Private Function ParseAddress(input As String) As Address
        Dim newAddress As New Address()
        newAddress.FullAddress = input
        If Not String.IsNullOrWhiteSpace(input) Then
            Dim matches As MatchCollection
            If _addressRegex_Compiled Is Nothing Then
                matches = Regex.Matches(input, GetAddressPattern(), RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.IgnoreCase)
            Else
                matches = _addressRegex_Compiled.Matches(input)
            End If
            If matches.Count = 1 Then
                Dim match As Match = matches(0)

                If match.Success Then

                    newAddress.Number = GetNormalizedValueForField(match, GroupNames.Number)
                    newAddress.PreDirectional = GetNormalizedValueForField(match, GroupNames.PreDirectional)
                    newAddress.Street = GetNormalizedValueForField(match, GroupNames.Street)
                    newAddress.Suffix = GetNormalizedValueForField(match, GroupNames.Suffix)
                    newAddress.PostDirectional = GetNormalizedValueForField(match, GroupNames.PostDirectional)
                    newAddress.SecondaryUnit = GetNormalizedValueForField(match, GroupNames.SecondaryUnit)
                    newAddress.SecondaryNumber = GetNormalizedValueForField(match, GroupNames.SecondaryNumber)
                    newAddress.POBoxNumber = GetNormalizedValueForField(match, GroupNames.POBoxNumber)
                    newAddress.City = GetNormalizedValueForField(match, GroupNames.City)
                    newAddress.County = GetNormalizedValueForField(match, GroupNames.County)
                    newAddress.State = GetNormalizedValueForField(match, GroupNames.State)
                    newAddress.ZipPlus4 = GetNormalizedValueForField(match, GroupNames.ZipPlus4)
                    newAddress.Country = GetNormalizedValueForField(match, GroupNames.Country)

                    'if there is a secondary number but no secondary unit then unit is apt
                    If Not String.IsNullOrWhiteSpace(newAddress.SecondaryNumber) AndAlso String.IsNullOrWhiteSpace(newAddress.SecondaryUnit) Then
                        newAddress.SecondaryUnit = "APT"
                    End If

                    FillEmptyBasedOnZip(newAddress)
                    FillEmptyBasedOnCityState(newAddress)

                    newAddress.FullAddress = BuildSingleLine(newAddress)

                End If
            End If
        End If

        Return newAddress
    End Function

    Private Sub FillEmptyBasedOnZip(dto As Address)
        If Not String.IsNullOrWhiteSpace(dto.ZipPlus4) AndAlso _masterCodeSet IsNot Nothing Then
            Dim zipcode As String = String.Empty
            Dim idx As Integer = dto.ZipPlus4.IndexOf("-")
            If idx > -1 Then
                zipcode = dto.ZipPlus4.Substring(0, idx)
            Else
                zipcode = dto.ZipPlus4
            End If

            Dim zipDto As ZipcodeCode = _masterCodeSet.ZipcodeCodes.FirstOrDefault(Function(d) d.Zipcode = zipcode)
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.City) Then dto.City = zipDto.City
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.State) Then dto.State = zipDto.StateCode
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.County) Then dto.County = zipDto.County
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.Country) Then dto.Country = "UNITED STATES"
        End If
    End Sub

    Private Sub FillEmptyBasedOnCityState(dto As Address)
        If _masterCodeSet IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(dto.City) AndAlso Not String.IsNullOrWhiteSpace(dto.State) Then
            Dim zipDto As ZipcodeCode = _masterCodeSet.ZipcodeCodes.FirstOrDefault(Function(d) d.StateCode = dto.State And String.Compare(d.City, dto.City, True) = 0)
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.ZipPlus4) Then dto.ZipPlus4 = zipDto.Zipcode
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.County) Then dto.County = zipDto.County
            If zipDto IsNot Nothing AndAlso String.IsNullOrWhiteSpace(dto.Country) Then dto.Country = "UNITED STATES"
        End If
    End Sub

    Private Function GetNormalizedValueForField(match As Match, GroupName As String) As String
        Dim value As String = match.Groups(GroupName).Value
        Dim result As String = value

        Select Case GroupName
            Case GroupNames.PreDirectional, GroupNames.PostDirectional
                result = GetCodeAsNormalizedValue(_masterCodeSet.DirectionalCodes, _masterCodeSet.DirectionalAliases, value)
            Case GroupNames.Suffix
                result = GetCodeAsNormalizedValue(_masterCodeSet.StreetSuffixCodes, _masterCodeSet.StreetSuffixAliases, value)
            Case GroupNames.SecondaryUnit
                result = GetCodeAsNormalizedValue(_masterCodeSet.StreetSecondaryUnitCodes, _masterCodeSet.StreetSecondaryUnitAliases, value)
            Case GroupNames.State
                result = GetCodeAsNormalizedValue(_masterCodeSet.StateCodes, _masterCodeSet.StateAliases, value)
            Case GroupNames.Country
                result = GetDescriptionAsNormalizedValue(_masterCodeSet.CountryCodes, _masterCodeSet.CountryAliases, value)
            Case GroupNames.Number
                If Not value.Contains("/"c) Then
                    result = value.Replace(" ", String.Empty)
                End If
            Case Else
                'do nothing
        End Select
        result = Regex.Replace(result, "^\s+|\s+$|[^\/\w\s\-\#\&]", String.Empty)
        Return result
    End Function

    Private Function GetCodeAsNormalizedValue(codes As IEnumerable(Of ICode), aliases As IEnumerable(Of ICodeAlias), value As String) As String
        Dim result As String = value
        Dim codeDto As ICode = codes.FirstOrDefault(Function(x) String.Compare(x.Description, value, True) = 0)
        If codeDto IsNot Nothing Then
            result = codeDto.Code
            If result Is Nothing Then result = value
        Else
            Dim aliasDto As ICodeAlias = aliases.FirstOrDefault(Function(x) String.Compare(x.AliasName, value, True) = 0)
            If aliasDto IsNot Nothing Then
                If aliasDto.Id.HasValue Then result = codes.FirstOrDefault(Function(x) x.Id.Value = aliasDto.ForeignId.Value).Code
                If result Is Nothing Then result = value
            End If
        End If
        Return result
    End Function

    Private Function GetDescriptionAsNormalizedValue(codes As IEnumerable(Of ICode), aliases As IEnumerable(Of ICodeAlias), value As String) As String
        Dim result As String = value
        Dim codeDto As ICode = codes.FirstOrDefault(Function(x) String.Compare(x.Code, value, True) = 0)
        If codeDto IsNot Nothing Then
            result = codeDto.Description
            If result Is Nothing Then result = value
        Else
            Dim aliasDto As ICodeAlias = aliases.FirstOrDefault(Function(x) String.Compare(x.AliasName, value, True) = 0)
            If aliasDto IsNot Nothing Then
                If aliasDto.Id.HasValue Then result = codes.FirstOrDefault(Function(x) x.Id.Value = aliasDto.ForeignId.Value).Description
                If result Is Nothing Then result = value
            End If
        End If
        Return result
    End Function

    Private Function GetAddressPattern() As String
        Dim directionalPattern As String = CreateDirectionalPattern()
        Dim suffixPattern As String = CreateStreetSuffixPattern()
        Dim rangedSecondaryUnitPattern As String = CreateSecondaryUnitPattern(True)
        Dim rangelessSecondaryUnitPattern As String = CreateSecondaryUnitPattern(False)
        Dim statePattern As String = CreateStatePattern()
        Dim zipPattern As String = CreateZipcodePattern()
        Dim countryPattern As String = CreateCountryPattern()
        Dim numberPattern As String = CreateNumberPattern()
        Dim allSecondaryUnitPattern As String = CreateSecondaryUnitPattern(rangedSecondaryUnitPattern, rangelessSecondaryUnitPattern)
        Dim directionStreetPattern As String = CreateStreetNameSameAsDirectionPattern(directionalPattern, suffixPattern, allSecondaryUnitPattern)
        Dim streetPattern As String = CreateStreetPattern(directionalPattern, allSecondaryUnitPattern, suffixPattern)
        Dim cityAndStatePattern As String = CreateCityStatePattern(statePattern)
        Dim placePattern As String = CreatePlacePattern(cityAndStatePattern, zipPattern, countryPattern)
        Dim poBoxPattern As String = CreatePOBoxPattern()
        Return CreateAddressPattern(numberPattern, directionStreetPattern, streetPattern, poBoxPattern, placePattern, zipPattern)
    End Function

    Private Function BuildCompiledRegex() As Regex

        Dim reg As Regex = New Regex(GetAddressPattern, RegexOptions.Compiled Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.IgnoreCase)
        reg.Match("")
        Return reg

    End Function

#Region "RegEx Patterns"

    Private Function CreateDirectionalPattern() As String

        Dim directionals As New List(Of String)
        directionals.AddRange(_masterCodeSet.DirectionalCodes.Select(Function(x) Regex.Escape(x.Code.ToUpper)))
        directionals.AddRange(_masterCodeSet.DirectionalCodes.Select(Function(x) Regex.Escape(x.Description.ToUpper)))
        directionals.AddRange(_masterCodeSet.DirectionalAliases.Select(Function(x) Regex.Escape(x.AliasName.ToUpper)))

        If _displayPatternsForTesting Then
            If _useReducedPatternForTesting Then
                ShowStringInTextEditor("NE|N", "directionalPattern")
                Return "NE|N"
            Else
                Dim directionalPattern As String = String.Join("|", directionals.Distinct.OrderByDescending(Function(x) x.Length))
                ShowStringInTextEditor(directionalPattern, "directionalPattern")
                Return directionalPattern
            End If
        Else
            If _useReducedPatternForTesting Then
                Return "NE|N"
            Else
                Return String.Join("|", directionals.Distinct.OrderByDescending(Function(x) x.Length))
            End If
        End If

    End Function

    Private Function CreateStreetSuffixPattern() As String

        Dim suffixes As New List(Of String)
        suffixes.AddRange(_masterCodeSet.StreetSuffixCodes.Select(Function(x) Regex.Escape(x.Code.ToUpper)))
        suffixes.AddRange(_masterCodeSet.StreetSuffixCodes.Select(Function(x) Regex.Escape(x.Description.ToUpper)))
        suffixes.AddRange(_masterCodeSet.StreetSuffixAliases.Select(Function(x) Regex.Escape(x.AliasName.ToUpper)))

        If _displayPatternsForTesting Then
            If _useReducedPatternForTesting Then
                ShowStringInTextEditor("ST", "streetSuffixPattern")
                Return "ST"
            Else
                Dim suffixPattern As String = String.Join("|", suffixes.Distinct.OrderByDescending(Function(x) x.Length))
                ShowStringInTextEditor(suffixPattern, "streetSuffixPattern")
                Return suffixPattern
            End If
        Else
            If _useReducedPatternForTesting Then
                Return "ST"
            Else
                Return String.Join("|", suffixes.Distinct.OrderByDescending(Function(x) x.Length))
            End If
        End If

    End Function

    Private Function CreateSecondaryUnitPattern(IsRanged As Boolean) As String

        Dim units As New List(Of String)
        units.AddRange(_masterCodeSet.StreetSecondaryUnitCodes.Where(Function(y) y.RequiresRange = IsRanged).Select(Function(x) Regex.Escape(x.Code.ToUpper)))
        units.AddRange(_masterCodeSet.StreetSecondaryUnitCodes.Where(Function(y) y.RequiresRange = IsRanged).Select(Function(x) Regex.Escape(x.Description.ToUpper)))
        units.AddRange(_masterCodeSet.StreetSecondaryUnitAliases.Select(Function(x) Regex.Escape(x.AliasName.ToUpper)))

        If _displayPatternsForTesting Then
            If _useReducedPatternForTesting Then
                ShowStringInTextEditor("APT", "secondaryUnitPattern")
                Return "APT"
            Else
                Dim secondaryUnitPattern As String = String.Join("|", units.Distinct.OrderByDescending(Function(x) x.Length))
                ShowStringInTextEditor(secondaryUnitPattern, "secondaryUnitPattern")
                Return secondaryUnitPattern
            End If
        Else
            If _useReducedPatternForTesting Then
                Return "APT"
            Else
                Return String.Join("|", units.Distinct.OrderByDescending(Function(x) x.Length))
            End If
        End If

    End Function

    Private Function CreateStatePattern() As String

        Dim states As New List(Of String)
        states.AddRange(_masterCodeSet.StateCodes.Select(Function(x) Regex.Escape(x.StateCode.ToUpper)))
        states.AddRange(_masterCodeSet.StateCodes.Select(Function(x) Regex.Escape(x.Description.ToUpper)))
        states.AddRange(_masterCodeSet.StateAliases.Select(Function(x) Regex.Escape(x.AliasName.ToUpper)))

        If _displayPatternsForTesting Then
            If _useReducedPatternForTesting Then
                ShowStringInTextEditor("OH", "statePattern")
                Return "OH"
            Else
                Dim statePattern As String = String.Join("|", states.Distinct.OrderByDescending(Function(x) x.Length))
                ShowStringInTextEditor(statePattern, "statePattern")
                Return statePattern
            End If
        Else
            If _useReducedPatternForTesting Then
                Return "OH"
            Else
                Return String.Join("|", states.Distinct.OrderByDescending(Function(x) x.Length))
            End If
        End If

    End Function

    Private Function CreateZipcodePattern() As String

        Dim zipBuilder As New StringBuilder
        zipBuilder.Append("(?:GIR|[A-Z]\d[A-Z\d]??|[A-Z]{2}\d[A-Z\d]??)[\ ]??(?:\d[A-Z]{2})|") 'UK
        zipBuilder.Append("(?:[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(?:\d[ABCEGHJKLMNPRSTVWXYZ]\d)|") 'Canada
        zipBuilder.Append("(?:F-)(?:(?:2[A|B])|[0-9]{2})[0-9]{3}|") 'France
        zipBuilder.Append("(?:V-|I-)[0-9]{5}|") 'Italy
        zipBuilder.Append("[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})|") 'Netherlands
        zipBuilder.Append("(?:[D-d][K-k])(?:\ |-)[1-9]{1}[0-9]{3}|") 'Denmark
        zipBuilder.Append("(?:s-|S-)[0-9]{3}\s?[0-9]{2}|") 'Sweden
        zipBuilder.Append("(?:[A-Z]\d{4}[A-Z]|(?:[A-Z]{2})\d{6})|") 'equador
        zipBuilder.Append("[A-Z]{2}[\ ]?[A-Z0-9]{2}|") 'bermuda
        zipBuilder.Append("(?:[A-HJ-NP-Z])\d{4}(?:[A-Z]{3})|") 'argentia
        zipBuilder.Append("\d{2,6}(?:[\ -]\d{3,5})|") 'US and others
        zipBuilder.Append("\d{4,6}") 'US and others

        If _displayPatternsForTesting Then
            Dim zipPattern As String = zipBuilder.ToString
            ShowStringInTextEditor(zipPattern, "zipPattern")
            Return zipPattern
        Else
            Return zipBuilder.ToString
        End If

    End Function

    Private Function CreateCountryPattern() As String

        Dim countries As New List(Of String)
        countries.AddRange(_masterCodeSet.CountryCodes.Select(Function(x) Regex.Escape(x.Code.ToUpper)))
        countries.AddRange(_masterCodeSet.CountryCodes.Select(Function(x) Regex.Escape(x.Description.ToUpper)))
        countries.AddRange(_masterCodeSet.CountryAliases.Select(Function(x) Regex.Escape(x.AliasName.ToUpper)))

        If _displayPatternsForTesting Then
            If _useReducedPatternForTesting Then
                ShowStringInTextEditor("UNITED STATES", "countryPattern")
                Return "UNITED STATES"
            Else
                Dim countryPattern As String = String.Join("|", countries.Distinct.OrderByDescending(Function(x) x.Length))
                ShowStringInTextEditor(countryPattern, "countryPattern")
                Return countryPattern
            End If
        Else
            If _useReducedPatternForTesting Then
                Return "UNITED STATES"
            Else
                Return String.Join("|", countries.Distinct.OrderByDescending(Function(x) x.Length))
            End If
        End If

    End Function

    Private Function CreateNumberPattern() As String

        Dim numberBuilder As New StringBuilder
        numberBuilder.Append("(?:(?:") ' Unit-attached 
        numberBuilder.Append(String.Concat("(?:(?<", GroupNames.Number, ">\d+)-(?<", GroupNames.SecondaryNumber, ">[0-9][A-Z]*(?=\b)))"))
        numberBuilder.Append("|") 'or Unit-attached 
        numberBuilder.Append(String.Concat("(?:(?<", GroupNames.Number, ">\d+)-?(?<", GroupNames.SecondaryNumber, ">[A-Z](?=\b)))"))
        numberBuilder.Append("|") 'or Fractional
        numberBuilder.Append(String.Concat("(?<", GroupNames.Number, ">(?:\d+[\-\ ]?\d+)\d+[\-\ ]?\d+\/\d+)"))
        numberBuilder.Append("|") 'or Normal Number
        numberBuilder.Append(String.Concat("(?<", GroupNames.Number, ">\d+-?\d*)"))
        numberBuilder.Append("|") 'or Wisconsin/Illinois 
        numberBuilder.Append(String.Concat("(?<", GroupNames.Number, ">[NSWE]\ ?\d+\ ?[NSWE]\ ?\d+)"))
        numberBuilder.Append(String.Concat(")\b)"))

        If _displayPatternsForTesting Then
            Dim numberPattern As String = numberBuilder.ToString
            ShowStringInTextEditor(numberPattern, "numberPattern")
            Return numberPattern
        Else
            Return numberBuilder.ToString
        End If

    End Function

    Private Function CreateSecondaryUnitPattern(rangedSecondaryUnitPattern As String, rangelessSecondaryUnitPattern As String) As String

        Dim unitBuilder As New StringBuilder
        unitBuilder.Append(" (:?") ' [fist choice group] a ranged or rangeless unit
        unitBuilder.Append(String.Concat("(?:(?:(?<", GroupNames.SecondaryUnit, ">", rangedSecondaryUnitPattern, "|", rangelessSecondaryUnitPattern, ")(?![a-z])\s*)"))
        unitBuilder.Append("|") ' or a Pound sign
        unitBuilder.Append(String.Concat("(?<", GroupNames.SecondaryNumber, ">\#)\s*"))
        unitBuilder.Append(")") 'followed by a number
        unitBuilder.Append(String.Concat("(?<", GroupNames.SecondaryNumber, ">[\w-]+)", ")"))
        unitBuilder.Append("|") ' or [second choice group] just a rangeless unit
        unitBuilder.Append(String.Concat("(?<", GroupNames.SecondaryUnit, ">", rangelessSecondaryUnitPattern, ")\b"))

        If _displayPatternsForTesting Then
            Dim allSecondaryUnitPattern As String = unitBuilder.ToString
            ShowStringInTextEditor(allSecondaryUnitPattern, "allSecondaryUnitPattern")
            Return allSecondaryUnitPattern
        Else
            Return unitBuilder.ToString
        End If

    End Function

    ''' <summary>
    ''' Pattern for Street Name is a Direction
    ''' Example:  100 South St
    ''' </summary>
    Private Function CreateStreetNameSameAsDirectionPattern(directionalPattern As String, suffixPattern As String, secondaryUnitPattern As String) As String

        Dim directStreetBuilder As New StringBuilder 'addresses like 100 South Street"
        directStreetBuilder.Append(String.Concat(" (?:(?<", GroupNames.PreDirectional, ">", directionalPattern, ")\W+)?"))
        directStreetBuilder.Append(String.Concat(" (?<", GroupNames.Street, ">[^,;]*[^\d\W])"))
        directStreetBuilder.Append(String.Concat(" (?:[^\w,;]+(?<", GroupNames.Suffix, ">", suffixPattern, ")\b)"))
        directStreetBuilder.Append(String.Concat(" (?:[^\w,;]+(?<", GroupNames.PostDirectional, ">", directionalPattern, "))?"))
        directStreetBuilder.Append(String.Concat(" (?:\W+(?:", secondaryUnitPattern, "))?"))

        If _displayPatternsForTesting Then
            Dim directStreetPattern As String = directStreetBuilder.ToString
            ShowStringInTextEditor(directStreetPattern, "strictStreetPattern")
            Return directStreetPattern
        Else
            Return directStreetBuilder.ToString
        End If

    End Function

    Private Function CreateStreetPattern(directionalPattern As String, secondaryUnitPattern As String, suffixPattern As String) As String

        Dim streetBuilder As New StringBuilder 'PreDirect? Street Suffix PostDirect? Unit?
        streetBuilder.Append(String.Concat(" (?:(?<", GroupNames.PreDirectional, ">", directionalPattern, ")\W+)?"))
        streetBuilder.Append(String.Concat(" (?<", GroupNames.Street, ">[^,;]*[^\d\W])"))
        streetBuilder.Append(String.Concat(" (?:[^\w,;]+(?<", GroupNames.Suffix, ">", suffixPattern, ")\b)"))
        streetBuilder.Append(String.Concat(" (?:[^\w,;]+(?<", GroupNames.PostDirectional, ">", directionalPattern, "))?"))
        streetBuilder.Append(String.Concat(" (?:\W+(?:", secondaryUnitPattern, "))?"))
        streetBuilder.Append("|") ' or PreDirect? Street PostDirect Unit?
        streetBuilder.Append(String.Concat(" (?:(?<", GroupNames.PreDirectional, ">", directionalPattern, ")\W+)?"))
        streetBuilder.Append(String.Concat(" (?<", GroupNames.Street, ">[^,;]*[^\d\W])"))
        streetBuilder.Append(String.Concat(" (?:[^\w,;]+(?<", GroupNames.PostDirectional, ">", directionalPattern, "))"))
        streetBuilder.Append(String.Concat(" (?:\W+(?:", secondaryUnitPattern, "))?"))
        streetBuilder.Append("|") ' or PreDirect? Street Unit
        streetBuilder.Append(String.Concat(" (?:(?<", GroupNames.PreDirectional, ">", directionalPattern, ")\W+)?"))
        streetBuilder.Append(String.Concat(" (?<", GroupNames.Street, ">[^,;]*[^\d\W])"))
        streetBuilder.Append(String.Concat(" \W+(?:", secondaryUnitPattern, ")"))
        streetBuilder.Append("|") ' or PreDirect? Street
        streetBuilder.Append(String.Concat(" (?:(?<", GroupNames.PreDirectional, ">", directionalPattern, ")\W+)?"))
        streetBuilder.Append(String.Concat(" (?<", GroupNames.Street, ">[^,;]*[^\d\W])"))

        If _displayPatternsForTesting Then
            Dim streetPattern As String = streetBuilder.ToString
            ShowStringInTextEditor(streetPattern, "strictStreetPattern")
            Return streetPattern
        Else
            Return streetBuilder.ToString
        End If

    End Function

    Private Function CreatePOBoxPattern() As String

        If _displayPatternsForTesting Then
            Dim poBoxPattern As String = String.Concat("(P[\.\ ]?O[\.\ ]?\ )?BOX\ \#?(?<", GroupNames.POBoxNumber, ">[0-9]+\w*\b)")
            ShowStringInTextEditor(poBoxPattern, "poBoxPattern")
            Return poBoxPattern
        Else
            Return String.Concat("(P[\.\ ]?O[\.\ ]?\ )?BOX\ \#?(?<", GroupNames.POBoxNumber, ">[0-9]+\w*\b)")
        End If

    End Function

    Private Function CreateCityStatePattern(statePattern As String) As String

        If _displayPatternsForTesting Then
            Dim cityAndStatePattern As String = String.Concat("(?<", GroupNames.City, ">[^\d,;]+?)\W+(?<", GroupNames.State, ">\b(?:", statePattern, ")\b)")
            ShowStringInTextEditor(cityAndStatePattern, "cityAndStatePattern")
            Return cityAndStatePattern
        Else
            Return String.Concat("(?<", GroupNames.City, ">[^\d,;]+?)\W+(?<", GroupNames.State, ">\b(?:", statePattern, ")\b)")
        End If

    End Function

    Private Function CreatePlacePattern(cityAndStatePattern As String, zipPattern As String, countryPattern As String) As String

        Dim placeBuilder As New StringBuilder
        placeBuilder.Append(String.Concat("(?:", cityAndStatePattern, ")\W*(?<", GroupNames.ZipPlus4, ">", zipPattern, ")\W*(?<", GroupNames.Country, ">", countryPattern, ")?")) 'city, state, zip country?
        placeBuilder.Append("|")
        placeBuilder.Append(String.Concat("(?:", cityAndStatePattern, ")\W*(?<", GroupNames.ZipPlus4, ">", zipPattern, ")")) 'city, state, zip
        placeBuilder.Append("|")
        placeBuilder.Append(String.Concat("(?:", cityAndStatePattern, ")\W*")) 'city, state
        placeBuilder.Append("|")
        placeBuilder.Append(String.Concat("(?<", GroupNames.ZipPlus4, ">", zipPattern, ")")) 'zip

        If _displayPatternsForTesting Then
            Dim placePattern As String = placeBuilder.ToString
            ShowStringInTextEditor(placePattern, "placePattern")
            Return placePattern
        Else
            Return placeBuilder.ToString
        End If


    End Function

    Private Function CreateAddressPattern(numberPattern As String, directionStreetPattern As String, streetPattern As String, poBoxPattern As String, placePattern As String, zipPattern As String) As String

        Dim addressBuilder As New StringBuilder

        'case for rural route
        addressBuilder.Append("(?<TopLevel_Rural>^")
        addressBuilder.Append(String.Concat("\W*(?<", GroupNames.Street, ">(RURAL ROUTE|RURAL|RFD|RD|RR).*?BOX\s*[^,;]*?\b)"))
        addressBuilder.Append(String.Concat("\W+(?:", placePattern, ")"))
        addressBuilder.Append("\W*")
        addressBuilder.Append("$)")
        'case for APO/FPO/DPO addresses
        addressBuilder.Append("|")
        addressBuilder.Append("(?<TopLevel_Military>^")
        addressBuilder.Append("[^\w\#]*")
        addressBuilder.Append(String.Concat("(?<", GroupNames.Street, ">.+?)"))
        addressBuilder.Append(String.Concat("(?<", GroupNames.City, ">[AFD]PO)\W+"))
        addressBuilder.Append(String.Concat("(?<", GroupNames.State, ">A[AEP])\W+"))
        addressBuilder.Append(String.Concat("(?<", GroupNames.ZipPlus4, ">", zipPattern, ")"))
        addressBuilder.Append("\W*")
        addressBuilder.Append("$)")
        'case for PO boxes
        addressBuilder.Append("|")
        addressBuilder.Append("(?<TopLevel_PoBox>^")
        addressBuilder.Append("\W*")
        addressBuilder.Append(String.Concat("(?:", poBoxPattern, ")"))
        addressBuilder.Append(String.Concat("(?:\W+(?:", placePattern, "))?"))
        addressBuilder.Append("\W*")
        addressBuilder.Append("$)")
        'directional as street name 
        addressBuilder.Append("|")
        addressBuilder.Append("(?<TopLevel_StrictStreet>^")
        addressBuilder.Append("[^\w\#;]*") 'skip non-word chars except (eg unit)
        addressBuilder.Append(String.Concat("(?:", numberPattern, ")? \W*"))
        addressBuilder.Append(String.Concat("(?:", directionStreetPattern, ")\W+"))
        addressBuilder.Append(String.Concat("(?:", placePattern, ")"))
        addressBuilder.Append("\W*")  'require on non-word chars at end
        addressBuilder.Append("$)")
        'place only (city state zip only addresses)
        addressBuilder.Append("|")
        addressBuilder.Append("(?<TopLevel_PlaceOnly>^")
        addressBuilder.Append("\s*")
        addressBuilder.Append(String.Concat("(?:", placePattern, ")"))
        addressBuilder.Append("\s*")
        addressBuilder.Append("$)")
        'standard case
        addressBuilder.Append("|")
        addressBuilder.Append("(?<TopLevel_Standard>^")
        addressBuilder.Append("[^\w\#;]*") 'skip non-word chars except (eg unit)
        addressBuilder.Append(String.Concat("(?:", numberPattern, ")? \W*"))
        addressBuilder.Append(String.Concat("(?:", streetPattern, ")\W+"))
        addressBuilder.Append(String.Concat("(?:", poBoxPattern, "\W+)?"))
        addressBuilder.Append(String.Concat("(?:", placePattern, ")"))
        addressBuilder.Append("\W*")  'require on non-word chars at end
        addressBuilder.Append("$)")
        'street only
        addressBuilder.Append("|")
        addressBuilder.Append("(?<TopLevel_StreetOnly>^")
        addressBuilder.Append("[^\w\#]*")  'skip non-word chars except (eg unit)
        addressBuilder.Append(String.Concat("(?:", numberPattern, ")? \W*"))
        addressBuilder.Append(String.Concat("(?:", streetPattern, ")\W*"))
        addressBuilder.Append("$)")

        If _displayPatternsForTesting Then
            Dim addressPattern_prep As String = addressBuilder.ToString
            ShowStringInTextEditor(addressPattern_prep, "addressPattern")
            Return addressPattern_prep
        Else
            Return addressBuilder.ToString
        End If

    End Function

#End Region

    ''' <inheritdoc/>
    Public Function BuildStreetLine(dto As Address) As String Implements IAddressTransformer.BuildStreetLine
        Return BuildStreetLine(dto.Number, dto.PreDirectional, dto.Street, dto.Suffix, dto.PostDirectional, dto.SecondaryUnit, dto.SecondaryNumber)
    End Function

    Private Function BuildStreetLine(number As String, preDirectional As String, street As String, suffix As String, postDirectional As String, secondaryUnit As String, secondaryNumber As String) As String
        Dim returnValue As String = Nothing
        Dim streetLine_1 As String = String.Join(" ", {number, preDirectional, street, suffix, postDirectional, secondaryUnit, secondaryNumber})
        returnValue = Regex.Replace(streetLine_1, "\ +", " ").Trim()
        Return returnValue
    End Function

    ''' <inheritdoc/>
    Public Function BuildSingleLine(dto As Address) As String Implements IAddressTransformer.BuildSingleLine
        Return BuildSingleLine(BuildStreetLine(dto), dto.POBoxNumber, dto.City, dto.State, dto.ZipPlus4, dto.Country)
    End Function

    Private Function BuildSingleLine(streetLine As String, poBox As String, city As String, state As String, zipPlus4 As String, country As String) As String 'Implements IAddressDtoTransformer.BuildSingleLine
        Dim returnValue As New StringBuilder

        If Not String.IsNullOrWhiteSpace(streetLine) Then
            returnValue.Append(streetLine)
        End If

        If Not String.IsNullOrWhiteSpace(poBox) Then
            If returnValue.Length > 0 Then returnValue.Append("; ")
            returnValue.Append("PO Box " & poBox)
        End If

        If Not String.IsNullOrWhiteSpace(city) Then
            If returnValue.Length > 0 Then returnValue.Append("; ")
            returnValue.Append(city)
        End If

        If Not String.IsNullOrWhiteSpace(state) Then
            If returnValue.Length > 0 Then returnValue.Append(" ")
            returnValue.Append(state)
        End If

        If Not String.IsNullOrWhiteSpace(zipPlus4) Then
            If returnValue.Length > 0 Then returnValue.Append("  ")
            returnValue.Append(zipPlus4)
        End If

        If Not String.IsNullOrWhiteSpace(country) Then
            If country.ToUpper <> "US" AndAlso country.ToUpper <> "UNITED STATES" Then
                If returnValue.Length > 0 Then returnValue.Append("; ")
                returnValue.Append(country)
            End If
        End If

        Return returnValue.ToString

    End Function

    ''' <inheritdoc/>
    Public Function BuildMultiline(dto As Address) As String Implements IAddressTransformer.BuildMultiline
        Return BuildMultiline(BuildStreetLine(dto), dto.POBoxNumber, dto.City, dto.State, dto.ZipPlus4, dto.Country)
    End Function

    ''' <inheritdoc/>
    Public Function BuildMultiline(fullParseAddress As String) As String Implements IAddressTransformer.BuildMultiline
        Dim tmp As List(Of String) = Split(fullParseAddress, ";").ToList
        Dim multiLine As String = String.Empty
        If tmp IsNot Nothing Then
            For Each s As String In tmp
                multiLine &= s & vbNewLine
            Next
        End If
        Return multiLine
    End Function

    Private Function BuildMultiline(streetLine As String, poBox As String, city As String, state As String, zipPlus4 As String, country As String) As String 'Implements IAddressDtoTransformer.BuildMultiline

        Dim returnValue As New StringBuilder

        If Not String.IsNullOrWhiteSpace(streetLine) Then
            returnValue.Append(streetLine & vbNewLine)
        End If

        If Not String.IsNullOrWhiteSpace(poBox) Then
            returnValue.Append("PO Box " & poBox & vbNewLine)
        End If

        If Not String.IsNullOrWhiteSpace(city) Then
            returnValue.Append(city)
        End If

        If Not String.IsNullOrWhiteSpace(state) Then
            If returnValue.Length > 0 Then returnValue.Append(" ")
            returnValue.Append(state)
        End If

        If Not String.IsNullOrWhiteSpace(zipPlus4) Then
            If returnValue.Length > 0 Then returnValue.Append("  ")
            returnValue.Append(zipPlus4)
        End If

        If Not String.IsNullOrWhiteSpace(country) Then
            If returnValue.Length > 0 Then returnValue.Append(" ")
            If country.ToUpper <> "US" AndAlso country.ToUpper <> "UNITED STATES" Then returnValue.Append(country)
        End If

        Return returnValue.ToString
    End Function


End Class

''' <summary>
''' Main Class for an Object to Transforms Addresses
''' </summary>
Public Interface IAddressTransformer
    Inherits IDataTransformer(Of Address)

    ''' <summary>
    ''' Builds Street Line of an Address (ie 123 Any Street)
    ''' </summary>
    ''' <param name="dto">Address as <see cref="Address"/></param>
    ''' <returns>Address as <see cref="String"/></returns>
    Function BuildStreetLine(dto As Address) As String

    ''' <summary>
    ''' Builds Address as a Single Line (ie 123 Any Ave MyCity ST Z2345)
    ''' </summary>
    ''' <param name="dto">Address as <see cref="Address"/></param>
    ''' <returns>Address as <see cref="String"/></returns>
    Function BuildSingleLine(dto As Address) As String

    ''' <summary>
    ''' Builds Address as a Multi Line (ie Line 1:123 Any Ave Line 2:MyCity ST Z2345)
    ''' </summary>
    ''' <param name="dto">Address as <see cref="Address"/></param>
    ''' <returns>Address as <see cref="String"/></returns>
    Function BuildMultiline(dto As Address) As String

    ''' <summary>
    ''' Builds Address as a Multi Line (ie Line 1:123 Any Ave Line 2:MyCity ST Z2345)
    ''' </summary>
    ''' <param name="fullParseAddress">Address as <see cref="String"/></param>
    ''' <returns>Address as <see cref="String"/></returns>
    Function BuildMultiline(fullParseAddress As String) As String

    ''' <summary>
    ''' Parse an Address from a Loosely Formatted String
    ''' </summary>
    ''' <param name="value">Address as <see cref="String"/></param>
    ''' <returns>Address as <see cref="Address"/></returns>
    Function ParseAsync(value As String) As Task(Of Address)

End Interface



