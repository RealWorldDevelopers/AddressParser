
namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   /// <summary>
   /// Entity DTO for County Field
   /// </summary>
   public interface ICountyCode : ICode
   {   
      /// <summary>
      /// State where County Resides
      /// </summary>
      string StateCode { get; set; }
   }

   /// <inheritdoc/>
   public class CountyCode : BaseCode, ICountyCode
   {
      /// <inheritdoc/>
      public string StateCode { get; set; }      

   }
}
