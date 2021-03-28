namespace RWD.Toolbox.Strings.Address
{
   /// <summary>
   /// Helper to assist in Building a Regular Expression
   /// </summary>
   public interface IRegExHelper
   {
      /// <summary>
      /// Pattern used to Parse and Validate a Direction
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string DirectionalPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Street Suffix
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string SuffixPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Ranged Secondary Unit
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string RangedSecondaryUnitPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Range-less Secondary Unit 
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string RangelessSecondaryUnitPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a States
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string StatePattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Postal Codes
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string PostalCodePattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Country
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string CountryPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Street Number
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string NumberPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Full Secondary Unit
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string AllSecondaryUnitPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Street Direction
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string DirectionStreetPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Street
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string StreetPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a City State Combo
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string CityAndStatePattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a Place
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string PlacePattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate a PO Box
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string POBoxPattern { get; }

      /// <summary>
      /// A Regular Expression to Parse and Validate an Address
      /// </summary>
      /// <returns>RegEx Pattern as <see cref="string"/></returns>
      string AddressPattern { get; }
   }
}
