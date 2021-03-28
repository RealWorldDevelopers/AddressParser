

namespace RWD.Toolbox.Strings.Address.Common.DTO
{


   /// <inheritdoc/>
   public class PostalCode : IPostalCode
   {
      /// <inheritdoc/>
      public int? Id { get; set; }
      /// <inheritdoc/>
      public string ZipCode { get; set; }
      /// <inheritdoc/>
      public string City { get; set; }
      /// <inheritdoc/>
      public string StateCode { get; set; }
      /// <inheritdoc/>
      public string County { get; set; }


      // TODO is this really needed

      /// <inheritdoc/>
      public string AreaCode { get; set; }
      /// <inheritdoc/>
      public string Latitude { get; set; }
      /// <inheritdoc/>
      public string Longitude { get; set; }

   }

}
