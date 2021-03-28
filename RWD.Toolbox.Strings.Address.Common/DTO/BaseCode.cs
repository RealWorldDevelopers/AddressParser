using System;

namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   /// <summary>
   /// A base class which can be used by most typical entity DTOs for code tables
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

      /// <summary>
      /// Returns the Description Property as the ToString Method
      /// </summary>
      /// <returns><see cref="String"/></returns>
      public override string ToString()
      {
         return Description;
      }

      /// <summary>
      /// Compare two Code Objects by Id
      /// </summary>
      /// <param name="obj">Object to Compare as <see cref="Object"/></param>
      /// <returns>Compare Results as <see cref="Boolean"/></returns>
      public override bool Equals(object obj)
      {
         // TODO is this needed?
         bool result = false;
         if (obj.GetType() == this.GetType())
         {
            BaseCode other = (BaseCode)obj;
            if (((other != null) && Id.HasValue && other.Id.HasValue && (other.Id.Value == Id.Value)))
               result = true;
         }
         return result;
      }

      public override int GetHashCode()
      {
         // TODO not sure what this is yet
         throw new NotImplementedException();
      }

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
