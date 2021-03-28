

namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   // TODO is clean up unused

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
      ///  County Associated with Zip
      /// </summary>
      string County { get; set; }   

      /// <summary>
      ///  State Code Associated with Zip
      /// </summary>
      string StateCode { get; set; }

      /// <summary>
      ///  Postal Zip Code
      /// </summary>
      string ZipCode { get; set; }


      // TODO is this really needed

      /// <summary>
      ///  Area Code Associated with Zip
      /// </summary>
      string AreaCode { get; set; }

      /// <summary>
      ///  Latitude Associated with Zip
      /// </summary>
      string Latitude { get; set; }

      /// <summary>
      ///  Longitude Associated with Zip
      /// </summary>
      string Longitude { get; set; }

   }

}
