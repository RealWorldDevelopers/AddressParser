
namespace RWD.Toolbox.Strings.Address.Models
{
   /// <summary>
   /// A base class which can be used by most DTOs for code tables
   /// </summary>
   /// <remarks>
   /// DO NOT assume that code entity DTOs inherit from this class.  If this 
   /// implementation is insufficient for a particular class of DTO, that class 
   /// should not inherit from this base class.  Rather, it should implement the 
   /// <see cref="ICode"/> interface directly, providing its own custom 
   /// implementation.
   /// </remarks>
   public abstract class BaseCode : ICode
   {
      /// <inheritdoc/>
      public int? Id { get; set; }

      /// <inheritdoc/>
      public string Code { get; set; }

      /// <inheritdoc/>
      public string Description { get; set; }

      /// <inheritdoc/>
      public bool RequiresRange { get; set; }

   }

   /// <inheritdoc/>
   public abstract class BaseAlias : ICodeAlias
   {
      /// <inheritdoc/>
      public int? Id { get; set; }
      /// <inheritdoc/>
      public string AliasName { get; set; }
      /// <inheritdoc/>
      public int? ForeignId { get; set; }
   }



}
