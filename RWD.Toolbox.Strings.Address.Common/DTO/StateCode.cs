
namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   /// <summary>
   /// Entity DTO for Postal State (ie OH, FL, PA)
   /// </summary>
   public interface IStateCode : ICode
   {
      /// <summary>
      /// Country where County Resides
      /// </summary>
      int? CountryCodeId { get; set; }
   }

   /// <inheritdoc/>
   public class StateCode : BaseCode, IStateCode
   {
      /// <inheritdoc/>
      public int? CountryCodeId { get; set; }

   }


   /// <inheritdoc/>
   public class StateAlias : BaseAlias { }

}
