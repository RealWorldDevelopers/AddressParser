
using System.Collections.Generic;

namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   /// <summary>
   ///  Set of Codes Needed for Address Control to Function
   /// </summary>
   public interface IMasterCodeSet
   {

      /// <summary>
      /// List of Direction Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of DirectionalCode)"/></returns>
      List<DirectionalCode> DirectionalCodes { get; }

      /// <summary>
      /// List of Directional Aliases Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of DirectionalAlias)"/></returns>
      List<DirectionalAlias> DirectionalAliases { get; }

      /// <summary>
      /// List of Street Suffix Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of StreetSuffixCode)"/></returns>
      List<StreetSuffixCode> StreetSuffixCodes { get; }

      /// <summary>
      /// List of Street Suffix Aliases Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of StreetSuffixAlias)"/></returns>
      List<StreetSuffixAlias> StreetSuffixAliases { get; }

      /// <summary>
      /// List of Street Secondary Unit Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of StreetSecondaryUnitCode)"/></returns>
      List<StreetSecondaryUnitCode> StreetSecondaryUnitCodes { get; }

      /// <summary>
      /// List of Street Secondary Unit Alias Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of StreetSecondaryUnitAlias)"/></returns>
      List<StreetSecondaryUnitAlias> StreetSecondaryUnitAliases { get; }


      /// <summary>
      /// List of State Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of StateCode)"/></returns>
      List<StateCode> StateCodes { get; }

      /// <summary>
      /// List of State Alias Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of StateAlias)"/></returns>
      List<StateAlias> StateAliases { get; }

      /// <summary>
      /// List of Country Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of CountryCode)"/></returns>
      List<CountryCode> CountryCodes { get; }

      /// <summary>
      /// List of Country Alias Codes Needed for Address Control to Function
      /// </summary>
      /// <returns>List of Codes as <see cref="List(Of CountryAlias)"/></returns>
      List<CountryAlias> CountryAliases { get; }

   }

}
