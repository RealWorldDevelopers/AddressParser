using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RWD.Toolbox.Strings.Address
{

   /// <summary>
   /// Format an Address
   /// </summary>
   public interface IFormatter
   {

      /// <summary>
      /// Builds Street Line of an Address (ie 123 Any Street)
      /// </summary>
      /// <param name="dto">Address as <see cref="Address"/></param>
      /// <returns>Address as <see cref="String"/></returns>
      string BuildStreetLine(Models.Address dto);

      /// <summary>
      /// Builds Address as a Single Line (ie 123 Any Ave MyCity ST Z2345)
      /// </summary>
      /// <param name="dto">Address as <see cref="Address"/></param>
      /// <returns>Address as <see cref="String"/></returns>
      string BuildSingleLine(Models.Address dto);

      /// <summary>
      /// Builds Address as a Multi Line (ie Line 1:123 Any Ave Line 2:MyCity ST Z2345)
      /// </summary>
      /// <param name="dto">Address as <see cref="Address"/></param>
      /// <returns>Address as <see cref="String"/></returns>
      string BuildMultiline(Models.Address dto);

      /// <summary>
      /// Builds Address as a Multi Line (ie Line 1:123 Any Ave Line 2:MyCity ST Z2345)
      /// </summary>
      /// <param name="fullParseAddress">Address as <see cref="String"/></param>
      /// <returns>Address as <see cref="String"/></returns>
      string BuildMultiline(string fullParseAddress);

   }


   /// <inheritdoc/>
   public class Formatter : IFormatter
   {
      readonly TextInfo _textInfoTool = CultureInfo.CurrentCulture.TextInfo;

      /// <inheritdoc/>
      public string BuildStreetLine(Models.Address dto)
      {
         return BuildStreetLine(dto.Number, dto.PreDirectional, dto.Street, dto.Suffix, dto.PostDirectional, dto.SecondaryUnit, dto.SecondaryNumber);
      }

      private string BuildStreetLine(string number, string preDirectional, string street, string suffix, string postDirectional, string secondaryUnit, string secondaryNumber)
      {
         var streetLine_1 = string.Join(" ", new[]
         {
            number.ToUpper(),
            preDirectional.ToUpper(),
            _textInfoTool.ToTitleCase(street),
            _textInfoTool.ToTitleCase(suffix),
            postDirectional.ToUpper(),
            _textInfoTool.ToTitleCase(secondaryUnit),
            secondaryNumber.ToUpper()
         });
         var returnValue = Regex.Replace(streetLine_1, @"\ +", " ").Trim();
         return returnValue;
      }

      /// <inheritdoc/>
      public string BuildSingleLine(Models.Address dto)
      {
         return BuildSingleLine(BuildStreetLine(dto), dto.POBoxNumber, dto.City, dto.State, dto.ZipPlus4, dto.Country);
      }

      private string BuildSingleLine(string streetLine, string poBox, string city, string state, string zipPlus4, string country) // Implements IAddressDtoTransformer.BuildSingleLine
      {
         StringBuilder returnValue = new StringBuilder();

         if (!string.IsNullOrWhiteSpace(streetLine))
            returnValue.Append(streetLine);

         if (!string.IsNullOrWhiteSpace(poBox))
         {
            if (returnValue.Length > 0)
               returnValue.Append("; ");
            returnValue.Append("PO Box " + poBox.ToUpper());
         }

         if (!string.IsNullOrWhiteSpace(city))
         {
            if (returnValue.Length > 0)
               returnValue.Append("; ");
            returnValue.Append(_textInfoTool.ToTitleCase(city));
         }

         if (!string.IsNullOrWhiteSpace(state))
         {
            if (returnValue.Length > 0)
               returnValue.Append(" ");
            returnValue.Append(_textInfoTool.ToTitleCase(state));
         }

         if (!string.IsNullOrWhiteSpace(zipPlus4))
         {
            if (returnValue.Length > 0)
               returnValue.Append("  ");
            returnValue.Append(zipPlus4.ToUpper());
         }

         if (!string.IsNullOrWhiteSpace(country))
         {
            if (country.ToUpper() != "US" && country.ToUpper() != "UNITED STATES")
            {
               if (returnValue.Length > 0)
                  returnValue.Append("; ");
               returnValue.Append(_textInfoTool.ToTitleCase(country));
            }
         }

         return returnValue.ToString();
      }

      /// <inheritdoc/>
      public string BuildMultiline(Models.Address dto)
      {
         return BuildMultiline(BuildStreetLine(dto), dto.POBoxNumber, dto.City, dto.State, dto.ZipPlus4, dto.Country);
      }

      /// <inheritdoc/>
      public string BuildMultiline(string fullParseAddress)
      {
         var tmp = fullParseAddress.Split(';').ToList();
         string multiLine = string.Empty;
         if (tmp != null)
         {
            foreach (string s in tmp)
               multiLine += s + Environment.NewLine;
         }
         return multiLine;
      }

      private string BuildMultiline(string streetLine, string poBox, string city, string state, string zipPlus4, string country)
      {
         StringBuilder returnValue = new StringBuilder();

         if (!string.IsNullOrWhiteSpace(streetLine))
            returnValue.Append(streetLine + Environment.NewLine);

         if (!string.IsNullOrWhiteSpace(poBox))
            returnValue.Append("PO Box " + poBox.ToUpper() + Environment.NewLine);

         if (!string.IsNullOrWhiteSpace(city))
            returnValue.Append(_textInfoTool.ToTitleCase(city));

         if (!string.IsNullOrWhiteSpace(state))
         {
            if (returnValue.Length > 0)
               returnValue.Append(" ");
            returnValue.Append(_textInfoTool.ToTitleCase(state));
         }

         if (!string.IsNullOrWhiteSpace(zipPlus4))
         {
            if (returnValue.Length > 0)
               returnValue.Append("  ");
            returnValue.Append(zipPlus4.ToUpper());
         }

         if (!string.IsNullOrWhiteSpace(country))
         {
            if (returnValue.Length > 0)
               returnValue.Append(" ");
            if (country.ToUpper() != "US" && country.ToUpper() != "UNITED STATES")
               returnValue.Append(_textInfoTool.ToTitleCase(country));
         }

         return returnValue.ToString();
      }

   }
}

