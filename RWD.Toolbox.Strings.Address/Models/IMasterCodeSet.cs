
using System.Collections.Generic;

namespace RWD.Toolbox.Strings.Address.Models
{
   /// <summary>
   ///  Set of Codes Needed for Address Control to Function
   /// </summary>
   public interface IMasterCodeSet
   {

      /// <summary>
      /// List of Direction Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{DirectionalCode}"/></returns>
      List<DirectionalCode> DirectionalCodes { get; }

      /// <summary>
      /// List of Directional Aliases Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{DirectionalAlias}"/></returns>
      List<DirectionalAlias> DirectionalAliases { get; }

      /// <summary>
      /// List of Street Suffix Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{StreetSuffixCode}"/></returns>
      List<StreetSuffixCode> StreetSuffixCodes { get; }

      /// <summary>
      /// List of Street Suffix Aliases Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{StreetSuffixAlias}"/></returns>
      List<StreetSuffixAlias> StreetSuffixAliases { get; }

      /// <summary>
      /// List of Street Secondary Unit Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{StreetSecondaryUnitCode}"/></returns>
      List<StreetSecondaryUnitCode> StreetSecondaryUnitCodes { get; }

      /// <summary>
      /// List of Street Secondary Unit Alias Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{StreetSecondaryUnitAlias}"/></returns>
      List<StreetSecondaryUnitAlias> StreetSecondaryUnitAliases { get; }


      /// <summary>
      /// List of State Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{StateCode}"/></returns>
      List<StateCode> StateCodes { get; }

      /// <summary>
      /// List of State Alias Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{StateAlias}"/></returns>
      List<StateAlias> StateAliases { get; }

      /// <summary>
      /// List of Country Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{CountryCode}"/></returns>
      List<CountryCode> CountryCodes { get; }

      /// <summary>
      /// List of Country Alias Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List{CountryAlias}"/></returns>
      List<CountryAlias> CountryAliases { get; }

   }

}
