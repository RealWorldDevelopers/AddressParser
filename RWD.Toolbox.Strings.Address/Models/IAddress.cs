namespace RWD.Toolbox.Strings.Address.Models
{
   /// <summary>
   /// IO DTO Used representing an Address
   /// </summary>
   public interface IAddress
   {
      /// <summary>
      /// The house number.
      /// </summary>
      string Number { get; set; }

      /// <summary>
      /// The pre-directional, such as "N" in "500 N Main St".
      /// </summary>
      string PreDirectional { get; set; }

      /// <summary>
      /// The name of the street, such as "Main" in "500 N Main St".
      /// </summary>
      string Street { get; set; }

      /// <summary>
      /// The street suffix, such as "ST" in "500 N MAIN ST".
      /// </summary>
      string Suffix { get; set; }

      /// <summary>
      /// The post-directional, such as "NW" in "500 Main St NW".
      /// </summary>
      string PostDirectional { get; set; }

      /// <summary>
      /// The secondary unit, such as "APT" in "500 N MAIN ST APT 3".
      /// </summary>
      string SecondaryUnit { get; set; }

      /// <summary>
      /// The secondary unit number, such as "3" in "500 N MAIN ST APT 3".
      /// </summary>
      string SecondaryNumber { get; set; }

      /// <summary>
      /// Post Office Box Number
      /// </summary>
      string POBoxNumber { get; set; }

      /// <summary>
      /// The city name.
      /// </summary>
      string City { get; set; }   

      /// <summary>
      /// The state or territory.
      /// </summary>
      string State { get; set; }

      /// <summary>
      /// The ZIP code.
      /// </summary>
      string ZipPlus4 { get; set; }

      /// <summary>
      /// The Country Code.
      /// </summary>
      string Country { get; set; }
   }
}