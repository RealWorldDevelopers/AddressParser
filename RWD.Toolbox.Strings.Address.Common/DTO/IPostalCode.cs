

namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   /// <summary>
   /// Entity DTO for Postal(Zip) codes
   /// </summary>
   public interface IPostalCode
   {
      /// <summary>
      ///  Unique Identifier for Postal Code
      /// </summary>
      int? Id { get; set; }

      /// <summary>
      ///  State Code Associated with Zip
      /// </summary>
      string City { get; set; }


      /// <summary>
      ///  State Code Associated with Zip
      /// </summary>
      string StateCode { get; set; }

      /// <summary>
      ///  Postal Zip Code
      /// </summary>
      string ZipCode { get; set; }

   }

}
