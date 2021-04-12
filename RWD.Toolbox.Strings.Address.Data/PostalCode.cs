
namespace RWD.Toolbox.Strings.Address.Data
{   
   /// <summary>
   /// Postal Code Data
   /// </summary> 
   public class PostalCode
   {
      /// <summary>
      /// Primary Key
      /// </summary>
      public int? Id { get; set; }

      /// <summary>
      /// ISO Country Code (2 characters)
      /// </summary>
      public string CountryCode { get; set; }

      /// <summary>
      /// Postal Code 
      /// </summary>
      public string Code { get; set; }

      /// <summary>
      /// State Name
      /// </summary>
      public string StateName { get; set; }

      /// <summary>
      /// State Code
      /// </summary>
      public string StateCode { get; set; }

      /// <summary>
      /// County or Province Name
      /// </summary>
      public string CountyProvinceName { get; set; }

      /// <summary>
      /// County or Province Code
      /// </summary>
      public string CountyProvinceCode { get; set; }    

   }
}
