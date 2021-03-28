using System;

namespace RWD.Toolbox.Strings.Address.Common.DTO
{
   /// <summary>
   /// An entity DTO for a code tables
   /// </summary>
   public interface ICode
   {

      /// <summary>
      /// The hidden unique ID for the code
      /// </summary>
      int? Id { get; set; }

      /// <summary>
      /// The short but meaningful abbreviation for the code which may be displayed to or entered by users
      /// </summary>
      string Code { get; set; }

      /// <summary>
      /// The full name or long description of the code which may be displayed to users
      /// </summary>
      string Description { get; set; }

      /// <summary>
      /// Does Unit Code expect a Range as a Value
      /// </summary>
      /// <returns>Unit Code expect a range as a value as <see cref="Boolean"/></returns>
      public bool RequiresRange { get; set; }
   }

   /// <summary>
   /// An entity DTO for aliases of a code table
   /// </summary>
   public interface ICodeAlias
   {
      /// <summary>
      /// The unique ID for the alias
      /// </summary>
      int? Id { get; set; }

      /// <summary>
      /// A commonly used abbreviation that is not an actual code 
      /// </summary>
      string AliasName { get; set; }

      /// <summary>
      /// Id of the Related Table
      /// </summary>
      int? ForeignId { get; set; }
   }

}
