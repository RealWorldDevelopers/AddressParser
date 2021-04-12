
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RWD.Toolbox.Strings.Address
{
   /// <summary>
   /// Parse a String into an Address
   /// </summary>
   public interface IParser
   {
      /// <summary>
      /// Parse a string value into an Address
      /// </summary>
      /// <param name="value">Address as a single line <see cref="string"/></param>
      /// <returns><see cref="Common.DTO.Address"/></returns>
      Common.DTO.Address Parse(string value);

      /// <summary>
      /// Use RegEx Option in compiled mode
      /// </summary>
      /// <remarks>
      /// Using RexEx in compiled mode will significantly increase performance with repeated use (ie in a loop).
      /// Use with this mode sparingly as it will cause a performance hit upfront before a benefit is realized.
      /// </remarks>
      bool UseCompiled { get; set; }
   }

   public class Parser : IParser
   {
      private readonly IRegExHelper _regExHelper;
      private readonly Common.DTO.IMasterCodeSet _masterCodeSet;
      private Regex _addressRegex_Compiled;

      /// <inheritdoc/>
      public bool UseCompiled { get; set; }

      /// <summary>
      /// Constructor 
      /// </summary>
      public Parser(IRegExHelper regExHelper, Common.DTO.IMasterCodeSet masterCodeSet)
      {
         _regExHelper = regExHelper;
         _masterCodeSet = masterCodeSet;
         // _addressRegex_Compiled = BuildCompiledRegex();
      }
         

      /// <inheritdoc/>
      public Common.DTO.Address Parse(string value)
      {
         var newAddress = new Common.DTO.Address();

         if (!string.IsNullOrWhiteSpace(value))
         {
            MatchCollection matches;
            if (!UseCompiled)
            {
               matches = Regex.Matches(value, _regExHelper.AddressPattern, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            }
            else
            {
               if (_addressRegex_Compiled == null)
               {
                  var masterPattern = _regExHelper.AddressPattern;
                  _addressRegex_Compiled = new Regex(masterPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
               }
               matches = _addressRegex_Compiled.Matches(value);
            }
            if (matches.Count == 1)
            {
               var match = matches[0];

               if (match.Success)
               {
                  newAddress.Number = GetNormalizedValueForField(match, RegExGroupNames.Number);
                  newAddress.PreDirectional = GetNormalizedValueForField(match, RegExGroupNames.PreDirectional);
                  newAddress.Street = GetNormalizedValueForField(match, RegExGroupNames.Street);
                  newAddress.Suffix = GetNormalizedValueForField(match, RegExGroupNames.Suffix);
                  newAddress.PostDirectional = GetNormalizedValueForField(match, RegExGroupNames.PostDirectional);
                  newAddress.SecondaryUnit = GetNormalizedValueForField(match, RegExGroupNames.SecondaryUnit);
                  newAddress.SecondaryNumber = GetNormalizedValueForField(match, RegExGroupNames.SecondaryNumber);
                  newAddress.POBoxNumber = GetNormalizedValueForField(match, RegExGroupNames.POBoxNumber);
                  newAddress.City = GetNormalizedValueForField(match, RegExGroupNames.City);
                  newAddress.State = GetNormalizedValueForField(match, RegExGroupNames.State);
                  newAddress.ZipPlus4 = GetNormalizedValueForField(match, RegExGroupNames.ZipPlus4);
                  newAddress.Country = GetNormalizedValueForField(match, RegExGroupNames.Country);

                  //if there is a secondary number but no secondary unit then unit is apt
                  if (!string.IsNullOrWhiteSpace(newAddress.SecondaryNumber) && string.IsNullOrWhiteSpace(newAddress.SecondaryUnit))
                  {
                     newAddress.SecondaryUnit = "APT";
                  }

               }
            }
         }

         return newAddress;

      }

      private string GetNormalizedValueForField(Match match, string groupName)
      {
         string value = match.Groups[groupName].Value;
         string result = value;

         switch (groupName)
         {
            case RegExGroupNames.PreDirectional:
            case RegExGroupNames.PostDirectional:
               {
                  result = GetCodeAsNormalizedValue(_masterCodeSet.DirectionalCodes, _masterCodeSet.DirectionalAliases, value);
                  break;
               }

            case RegExGroupNames.Suffix:
               {
                  result = GetCodeAsNormalizedValue(_masterCodeSet.StreetSuffixCodes, _masterCodeSet.StreetSuffixAliases, value);
                  break;
               }

            case RegExGroupNames.SecondaryUnit:
               {
                  result = GetCodeAsNormalizedValue(_masterCodeSet.StreetSecondaryUnitCodes, _masterCodeSet.StreetSecondaryUnitAliases, value);
                  break;
               }

            case RegExGroupNames.State:
               {
                  result = GetCodeAsNormalizedValue(_masterCodeSet.StateCodes, _masterCodeSet.StateAliases, value);
                  break;
               }

            case RegExGroupNames.Country:
               {
                  result = GetDescriptionAsNormalizedValue(_masterCodeSet.CountryCodes, _masterCodeSet.CountryAliases, value);
                  break;
               }

            case RegExGroupNames.Number:
               {
                  if (!value.Contains('/'))
                     result = value.Replace(" ", string.Empty);
                  break;
               }

            default:
               {
                  break;
               }
         }
         result = Regex.Replace(result, @"^\s+|\s+$|[^\/\w\s\-\#\&]", string.Empty);
         return result;
      }

      private string GetCodeAsNormalizedValue(IEnumerable<Common.DTO.ICode> codes, IEnumerable<Common.DTO.ICodeAlias> aliases, string value)
      {
         string result = value;
         var codeDto = codes.FirstOrDefault(x => string.Compare(x.Description, value, true) == 0);
         if (codeDto != null)
         {
            result = codeDto.Code;
            if (result == null)
               result = value;
         }
         else
         {
            var aliasDto = aliases.FirstOrDefault(x => string.Compare(x.AliasName, value, true) == 0);
            if (aliasDto != null)
            {
               if (aliasDto.Id.HasValue)
                  result = codes.FirstOrDefault(x => x.Id.Value == aliasDto.ForeignId.Value).Code;
               if (result == null)
                  result = value;
            }
         }
         return result;
      }

      private string GetDescriptionAsNormalizedValue(IEnumerable<Common.DTO.ICode> codes, IEnumerable<Common.DTO.ICodeAlias> aliases, string value)
      {
         string result = value;
         var codeDto = codes.FirstOrDefault(x => string.Compare(x.Code, value, true) == 0);
         if (codeDto != null)
         {
            result = codeDto.Description;
            if (result == null)
               result = value;
         }
         else
         {
            var aliasDto = aliases.FirstOrDefault(x => string.Compare(x.AliasName, value, true) == 0);
            if (aliasDto != null)
            {
               if (aliasDto.Id.HasValue)
                  result = codes.FirstOrDefault(x => x.Id.Value == aliasDto.ForeignId.Value).Description;
               if (result == null)
                  result = value;
            }
         }
         return result;
      }

   }

}
