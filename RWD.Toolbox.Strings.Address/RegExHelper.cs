
using RWD.Toolbox.Strings.Address.Common.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RWD.Toolbox.Strings.Address
{

   /// <inheritdoc/>
   public class RegExHelper : IRegExHelper
   {
      private readonly IMasterCodeSet _masterCodeSet;

      public RegExHelper(IMasterCodeSet masterCodeSet)
      {
         _masterCodeSet = masterCodeSet;

         // using TPL to parallel call gets
         List<Task> tasks = new List<Task>();
         var taskDirectionalPattern = Task.Run(() =>
            CreateDirectionalPattern());
         tasks.Add(taskDirectionalPattern);
         DirectionalPattern = taskDirectionalPattern.Result;

         var taskSuffixPattern = Task.Run(() =>
            CreateStreetSuffixPattern());
         tasks.Add(taskSuffixPattern);
         SuffixPattern = taskSuffixPattern.Result;

         var taskRangedSecondaryUnitPattern = Task.Run(() =>
            CreateSecondaryUnitPattern(true));
         tasks.Add(taskRangedSecondaryUnitPattern);
         RangedSecondaryUnitPattern = taskRangedSecondaryUnitPattern.Result;

         var taskRangelessSecondaryUnitPattern = Task.Run(() =>
            CreateSecondaryUnitPattern(false));
         tasks.Add(taskRangelessSecondaryUnitPattern);
         RangelessSecondaryUnitPattern = taskRangelessSecondaryUnitPattern.Result;

         var taskStatePattern = Task.Run(() =>
           CreateStatePattern());
         tasks.Add(taskStatePattern);
         StatePattern = taskStatePattern.Result;

         var taskPostalCodePattern = Task.Run(() =>
           CreatePostalCodePattern());
         tasks.Add(taskPostalCodePattern);
         PostalCodePattern = taskPostalCodePattern.Result;

         var taskCountryPattern = Task.Run(() =>
           CreateCountryPattern());
         tasks.Add(taskCountryPattern);
         CountryPattern = taskCountryPattern.Result;

         var taskNumberPattern = Task.Run(() =>
           CreateNumberPattern());
         tasks.Add(taskNumberPattern);
         NumberPattern = taskNumberPattern.Result;

         var taskPOBoxPattern = Task.Run(() =>
           CreatePOBoxPattern());
         tasks.Add(taskPOBoxPattern);
         POBoxPattern = taskPOBoxPattern.Result;

         Task.WaitAll(tasks.ToArray());

         var taskAllSecondaryUnitPattern = Task.Run(() =>
           CreateSecondaryUnitPattern(RangedSecondaryUnitPattern, RangelessSecondaryUnitPattern));
         tasks.Add(taskAllSecondaryUnitPattern);
         AllSecondaryUnitPattern = taskAllSecondaryUnitPattern.Result;

         var taskCityAndStatePattern = Task.Run(() =>
           CreateCityStatePattern(StatePattern));
         tasks.Add(taskCityAndStatePattern);
         CityAndStatePattern = taskCityAndStatePattern.Result;

         Task.WaitAll(tasks.ToArray());

         var taskDirectionStreetPattern = Task.Run(() =>
          CreateStreetNameSameAsDirectionPattern(DirectionalPattern, SuffixPattern, AllSecondaryUnitPattern));
         tasks.Add(taskDirectionStreetPattern);
         DirectionStreetPattern = taskDirectionStreetPattern.Result;

         var taskStreetPattern = Task.Run(() =>
          CreateStreetPattern(DirectionalPattern, AllSecondaryUnitPattern, SuffixPattern));
         tasks.Add(taskStreetPattern);
         StreetPattern = taskStreetPattern.Result;

         var taskPlacePattern = Task.Run(() =>
          CreatePlacePattern(CityAndStatePattern, PostalCodePattern, CountryPattern));
         tasks.Add(taskPlacePattern);
         PlacePattern = taskPlacePattern.Result;

         Task.WaitAll(tasks.ToArray());

         AddressPattern = CreateAddressPattern(NumberPattern, DirectionStreetPattern, StreetPattern, POBoxPattern, PlacePattern, PostalCodePattern);
      }

      public string DirectionalPattern { get; }
      public string SuffixPattern { get; }
      public string RangedSecondaryUnitPattern { get; }
      public string RangelessSecondaryUnitPattern { get; }
      public string StatePattern { get; }
      public string PostalCodePattern { get; }
      public string CountryPattern { get; }
      public string NumberPattern { get; }
      public string AllSecondaryUnitPattern { get; }
      public string DirectionStreetPattern { get; }
      public string StreetPattern { get; }
      public string CityAndStatePattern { get; }
      public string PlacePattern { get; }
      public string POBoxPattern { get; }

      public string AddressPattern { get; }




      /// <summary>
      /// Create RegEx Pattern for Direction
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateDirectionalPattern()
      {
         var directionals = new List<string>();
         directionals.AddRange(_masterCodeSet.DirectionalCodes.Select(c => Regex.Escape(c.Code.ToUpper())));
         directionals.AddRange(_masterCodeSet.DirectionalCodes.Select(c => Regex.Escape(c.Description.ToUpper())));
         directionals.AddRange(_masterCodeSet.DirectionalAliases.Select(c => Regex.Escape(c.AliasName.ToUpper())));

         var directionalPattern = string.Join("|", directionals.OrderByDescending(d => d.Length).Distinct());
         return directionalPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Street Suffix 
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateStreetSuffixPattern()
      {
         var suffixes = new List<string>();
         suffixes.AddRange(_masterCodeSet.StreetSuffixCodes.Select(c => Regex.Escape(c.Code.ToUpper())));
         suffixes.AddRange(_masterCodeSet.StreetSuffixCodes.Select(c => Regex.Escape(c.Description.ToUpper())));
         suffixes.AddRange(_masterCodeSet.StreetSuffixAliases.Select(c => Regex.Escape(c.AliasName.ToUpper())));

         var suffixPattern = string.Join("|", suffixes.OrderByDescending(s => s.Length).Distinct());
         return suffixPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Secondary Unit
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateSecondaryUnitPattern(bool isRanged)
      {
         var units = new List<string>();
         units.AddRange(_masterCodeSet.StreetSecondaryUnitCodes.Where(s => s.RequiresRange == isRanged).Select(c => Regex.Escape(c.Code.ToUpper())));
         units.AddRange(_masterCodeSet.StreetSecondaryUnitCodes.Where(s => s.RequiresRange == isRanged).Select(c => Regex.Escape(c.Description.ToUpper())));
         units.AddRange(_masterCodeSet.StreetSecondaryUnitAliases.Select(a => Regex.Escape(a.AliasName.ToUpper())));

         var secondaryUnitPattern = string.Join("|", units.OrderByDescending(u => u.Length).Distinct());
         return secondaryUnitPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for State
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateStatePattern()
      {
         var states = new List<string>();
         states.AddRange(_masterCodeSet.StateCodes.Select(s => Regex.Escape(s.Code.ToUpper())));
         states.AddRange(_masterCodeSet.StateCodes.Select(s => Regex.Escape(s.Description.ToUpper())));
         states.AddRange(_masterCodeSet.StateAliases.Select(a => Regex.Escape(a.AliasName.ToUpper())));

         var statePattern = string.Join("|", states.OrderByDescending(s => s.Length).Distinct());
         return statePattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Postal Codes
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreatePostalCodePattern()
      {
         var zipBuilder = new StringBuilder();
         zipBuilder.Append(@"(?:GIR|[A-Z]\d[A-Z\d]??|[A-Z]{2}\d[A-Z\d]??)[\ ]??(?:\d[A-Z]{2})|"); // UK
         zipBuilder.Append(@"(?:[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(?:\d[ABCEGHJKLMNPRSTVWXYZ]\d)|"); // Canada
         zipBuilder.Append(@"(?:F-)(?:(?:2[A|B])|[0-9]{2})[0-9]{3}|"); // France
         zipBuilder.Append(@"(?:V-|I-)[0-9]{5}|"); // Italy
         zipBuilder.Append(@"[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})|"); // Netherlands
         zipBuilder.Append(@"(?:[D-d][K-k])(?:\ |-)[1-9]{1}[0-9]{3}|"); // Denmark
         zipBuilder.Append(@"(?:s-|S-)[0-9]{3}\s?[0-9]{2}|"); // Sweden
         zipBuilder.Append(@"(?:[A-Z]\d{4}[A-Z]|(?:[A-Z]{2})\d{6})|"); // Ecuador
         zipBuilder.Append(@"[A-Z]{2}[\ ]?[A-Z0-9]{2}|"); // Bermuda
         zipBuilder.Append(@"(?:[A-HJ-NP-Z])\d{4}(?:[A-Z]{3})|"); // Argentinian
         zipBuilder.Append(@"\d{2,6}(?:[\ -]\d{3,5})|"); // US and others
         zipBuilder.Append(@"\d{4,6}"); // US and others

         var zipPattern = zipBuilder.ToString();
         return zipPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Country
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateCountryPattern()
      {
         var countries = new List<string>();
         countries.AddRange(_masterCodeSet.CountryCodes.Select(c => Regex.Escape(c.Code.ToUpper())));
         countries.AddRange(_masterCodeSet.CountryCodes.Select(c => Regex.Escape(c.Description.ToUpper())));
         countries.AddRange(_masterCodeSet.CountryAliases.Select(a => Regex.Escape(a.AliasName.ToUpper())));

         var countryPattern = string.Join("|", countries.OrderByDescending(c => c.Length).Distinct());
         return countryPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Street Number
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateNumberPattern()
      {
         var numberBuilder = new StringBuilder();
         numberBuilder.Append(@"(?:(?:"); // Unit-attached 
         numberBuilder.Append($@"(?:(?<{RegExGroupNames.Number}>\d+)-(?<{RegExGroupNames.SecondaryNumber}>[0-9][A-Z]*(?=\b)))");
         numberBuilder.Append("|"); // or Unit-attached 
         numberBuilder.Append($@"(?:(?<{RegExGroupNames.Number}>\d+)-?(?<{RegExGroupNames.SecondaryNumber}>[A-Z](?=\b)))");
         numberBuilder.Append("|"); // or Fractional
         numberBuilder.Append($@"(?<{RegExGroupNames.Number}>(?:\d+[\-\ ]?\d+)\d+[\-\ ]?\d+\/\d+)");
         numberBuilder.Append("|"); // or Normal Number
         numberBuilder.Append($@"(?<{RegExGroupNames.Number}>\d+-?\d*)");
         numberBuilder.Append("|"); // or Wisconsin/Illinois 
         numberBuilder.Append($@"(?<{RegExGroupNames.Number}>[NSWE]\ ?\d+\ ?[NSWE]\ ?\d+)");
         numberBuilder.Append(@")\b)");

         var numberPattern = numberBuilder.ToString();
         return numberPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Secondary Unit
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateSecondaryUnitPattern(string rangedSecondaryUnitPattern, string rangelessSecondaryUnitPattern)
      {
         var unitBuilder = new StringBuilder();
         unitBuilder.Append(@" (:?"); // [fist choice group] a ranged or range-less unit
         unitBuilder.Append($@"(?:(?:(?<{ RegExGroupNames.SecondaryUnit}>{rangedSecondaryUnitPattern}|{rangelessSecondaryUnitPattern})(?![a-z])\s*)");
         unitBuilder.Append("|"); // or a Pound sign
         unitBuilder.Append($@"(?<{RegExGroupNames.SecondaryNumber}>\#)\s*");
         unitBuilder.Append(")"); // followed by a number
         unitBuilder.Append($@"(?<{RegExGroupNames.SecondaryNumber}>[\w-]+))");
         unitBuilder.Append("|"); // or [second choice group] just a range-less unit
         unitBuilder.Append($@"(?<{RegExGroupNames.SecondaryUnit}>{rangelessSecondaryUnitPattern})\b");

         var allSecondaryUnitPattern = unitBuilder.ToString();
         return allSecondaryUnitPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Street Name that is a Direction
      /// Example:  100 South St
      /// </summary>
      private string CreateStreetNameSameAsDirectionPattern(string directionalPattern, string suffixPattern, string secondaryUnitPattern)
      {
         var directStreetBuilder = new StringBuilder(); // addresses like 100 South Street"
         directStreetBuilder.Append($@" (?:(?<{RegExGroupNames.PreDirectional}>{directionalPattern})\W+)?");
         directStreetBuilder.Append($@" (?<{RegExGroupNames.Street}>[^,;]*[^\d\W])");
         directStreetBuilder.Append($@" (?:[^\w,;]+(?<{RegExGroupNames.Suffix}>{suffixPattern})\b)");
         directStreetBuilder.Append($@" (?:[^\w,;]+(?<{RegExGroupNames.PostDirectional}>{directionalPattern}))?");
         directStreetBuilder.Append($@" (?:\W+(?:{secondaryUnitPattern}))?");

         var directStreetPattern = directStreetBuilder.ToString();
         return directStreetPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Street
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateStreetPattern(string directionalPattern, string secondaryUnitPattern, string suffixPattern)
      {
         var streetBuilder = new StringBuilder(); // PreDirect? Street Suffix PostDirect? Unit?
         streetBuilder.Append($@" (?:(?<{RegExGroupNames.PreDirectional}>{directionalPattern})\W+)?");
         streetBuilder.Append($@" (?<{RegExGroupNames.Street}>[^,;]*[^\d\W])");
         streetBuilder.Append($@" (?:[^\w,;]+(?<{RegExGroupNames.Suffix}>{suffixPattern})\b)");
         streetBuilder.Append($@" (?:[^\w,;]+(?<{RegExGroupNames.PostDirectional}>{directionalPattern}))?");
         streetBuilder.Append($@" (?:\W+(?:{secondaryUnitPattern}))?");
         streetBuilder.Append("|"); // or PreDirect? Street PostDirect Unit?
         streetBuilder.Append($@" (?:(?<{RegExGroupNames.PreDirectional}>{directionalPattern})\W+)?");
         streetBuilder.Append($@" (?<{RegExGroupNames.Street}>[^,;]*[^\d\W])");
         streetBuilder.Append($@" (?:[^\w,;]+(?<{RegExGroupNames.PostDirectional}>{directionalPattern}))");
         streetBuilder.Append($@" (?:\W+(?:{secondaryUnitPattern}))?");
         streetBuilder.Append("|"); // or PreDirect? Street Unit
         streetBuilder.Append($@" (?:(?<{RegExGroupNames.PreDirectional}>{directionalPattern})\W+)?");
         streetBuilder.Append($@" (?<{RegExGroupNames.Street}>[^,;]*[^\d\W])");
         streetBuilder.Append($@" \W+(?:{secondaryUnitPattern})");
         streetBuilder.Append("|"); // or PreDirect? Street
         streetBuilder.Append($@" (?:(?<{RegExGroupNames.PreDirectional}>{directionalPattern})\W+)?");
         streetBuilder.Append($@" (?<{RegExGroupNames.Street}>[^,;]*[^\d\W])");

         var streetPattern = streetBuilder.ToString();
         return streetPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for PO Box
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreatePOBoxPattern()
      {
         var poBoxPattern = $@"(P[\.\ ]?O[\.\ ]?\ )?BOX\ \#?(?<{RegExGroupNames.POBoxNumber}>[0-9]+\w*\b)";
         return poBoxPattern;
      }

      /// <summary>
      /// Create RegEx Pattern for City State Combo
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateCityStatePattern(string statePattern)
      {
         var cityAndStatePattern = $@"(?<{RegExGroupNames.City}>[^\d,;]+?)\W+(?<{RegExGroupNames.State}>\b(?:{statePattern})\b)";
         return cityAndStatePattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Place
      /// City State Country Zip
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreatePlacePattern(string cityAndStatePattern, string zipPattern, string countryPattern)
      {
         var placeBuilder = new StringBuilder();
         placeBuilder.Append($@"(?:{cityAndStatePattern})\W*(?<{RegExGroupNames.ZipPlus4}>{zipPattern})\W*(?<{RegExGroupNames.Country}>{countryPattern})?"); // city, state, zip country?
         placeBuilder.Append("|");
         placeBuilder.Append($@"(?:{cityAndStatePattern})\W*(?<{RegExGroupNames.ZipPlus4}>{zipPattern})"); // city, state, zip
         placeBuilder.Append("|");
         placeBuilder.Append($@"(?:{cityAndStatePattern})\W*"); // city, state
         placeBuilder.Append("|");
         placeBuilder.Append($@"(?<{RegExGroupNames.ZipPlus4}>{zipPattern})"); // zip

         var placePattern = placeBuilder.ToString();
         return placePattern;
      }

      /// <summary>
      /// Create RegEx Pattern for Address
      /// </summary>
      /// <returns><see cref="string"/></returns>
      private string CreateAddressPattern(string numberPattern, string directionStreetPattern, string streetPattern, string poBoxPattern, string placePattern, string zipPattern)
      {
         var addressBuilder = new StringBuilder();

         // case for rural route
         addressBuilder.Append(@"(?<TopLevel_Rural>^");
         addressBuilder.Append($@"\W*(?<{RegExGroupNames.Street}>(RURAL ROUTE|RURAL|RFD|RD|RR).*?BOX\s*[^,;]*?\b)");
         addressBuilder.Append($@"\W+(?:{placePattern})");
         addressBuilder.Append(@"\W*");
         addressBuilder.Append(@"$)");
         // case for APO/FPO/DPO addresses
         addressBuilder.Append("|");
         addressBuilder.Append(@"(?<TopLevel_Military>^");
         addressBuilder.Append(@"[^\w\#]*");
         addressBuilder.Append($@"(?<{RegExGroupNames.Street}>.+?)");
         addressBuilder.Append($@"(?<{RegExGroupNames.City}>[AFD]PO)\W+");
         addressBuilder.Append($@"(?<{RegExGroupNames.State}>A[AEP])\W+");
         addressBuilder.Append($@"(?<{RegExGroupNames.ZipPlus4}>{zipPattern})");
         addressBuilder.Append(@"\W*");
         addressBuilder.Append(@"$)");
         // case for PO boxes
         addressBuilder.Append("|");
         addressBuilder.Append(@"(?<TopLevel_PoBox>^");
         addressBuilder.Append(@"\W*");
         addressBuilder.Append($@"(?:{poBoxPattern})");
         addressBuilder.Append($@"(?:\W+(?:{placePattern}))?");
         addressBuilder.Append(@"\W*");
         addressBuilder.Append(@"$)");
         // directional as street name 
         addressBuilder.Append("|");
         addressBuilder.Append(@"(?<TopLevel_StrictStreet>^");
         addressBuilder.Append(@"[^\w\#;]*"); // skip non-word chars except (ie unit)
         addressBuilder.Append($@"(?:{numberPattern})? \W*");
         addressBuilder.Append($@"(?:{directionStreetPattern})\W+");
         addressBuilder.Append($@"(?:{placePattern})");
         addressBuilder.Append(@"\W*");// require on non-word chars at end
         addressBuilder.Append(@"$)");
         // place only (city state zip only addresses)
         addressBuilder.Append("|");
         addressBuilder.Append(@"(?<TopLevel_PlaceOnly>^");
         addressBuilder.Append(@"\s*");
         addressBuilder.Append($@"(?:{placePattern})");
         addressBuilder.Append(@"\s*");
         addressBuilder.Append(@"$)");
         // standard case
         addressBuilder.Append("|");
         addressBuilder.Append(@"(?<TopLevel_Standard>^");
         addressBuilder.Append(@"[^\w\#;]*"); // skip non-word chars except (ie unit)
         addressBuilder.Append($@"(?:{numberPattern})? \W*");
         addressBuilder.Append($@"(?:{streetPattern})\W+");
         addressBuilder.Append($@"(?:{poBoxPattern}\W+)?");
         addressBuilder.Append($@"(?:{placePattern})");
         addressBuilder.Append(@"\W*"); // require on non-word chars at end
         addressBuilder.Append(@"$)");
         // street only
         addressBuilder.Append("|");
         addressBuilder.Append(@"(?<TopLevel_StreetOnly>^");
         addressBuilder.Append(@"[^\w\#]*"); // skip non-word chars except (ie unit)
         addressBuilder.Append($@"(?:{numberPattern})? \W*");
         addressBuilder.Append($@"(?:{streetPattern})\W*");
         addressBuilder.Append(@"$)");

         var addressPattern_prep = addressBuilder.ToString();
         return addressPattern_prep;

      }


   }
}
