
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RWD.Toolbox.Strings.Address.Data.Admin
{
   public class Program
   {
      // http://bekenty.com/use-sqlite-in-net-core-3-with-entity-framework-core/

      static void Main(string[] args)
      {
         // instruct user
         PrintInstructions();

         Console.WriteLine();  // space    

         // collect path to json file to import
         Console.Write("Enter Path to JSON file to Import:");
         var path = Console.ReadLine();
         if (HelpRequired(path))
         {
            Console.WriteLine(@"Path to JSON example - c:\somedirectory\filename.json");
            Console.WriteLine();  // space
            Console.Write("Enter Path to JSON file to Import:");
            path = Console.ReadLine();
         }

         Console.WriteLine(); // space

         // collect which country to import
         Console.Write("Enter ISO Country Code to Import:");
         var countryCode = Console.ReadLine();
         if (HelpRequired(countryCode))
         {
            Console.WriteLine(@"ISO Country Code (2 letter) example - US");
            Console.WriteLine();  // space
            Console.Write("Enter Country Code to Import:");
            countryCode = Console.ReadLine();
         }

         // create json array from file
         List<PostalCode> fileData;
         using (StreamReader file = File.OpenText(path))
         {
            JsonSerializer serializer = new JsonSerializer();
            fileData = (List<PostalCode>)serializer.Deserialize(file, typeof(List<PostalCode>));
         }

         // filter array by country
         var dataByCountry = fileData.Where(d => d.CountryCode.ToLower() == countryCode.ToLower());

         using (var db = new SQLiteDBContext())
         {
            // delete existing country data
            var deletes = db.PostalCodes.Where(d => d.CountryCode.ToLower() == countryCode.ToLower());
            if (deletes.Any())
            {
               var recordCount = deletes.Count();
               db.RemoveRange(deletes.ToList());               
               db.SaveChanges();
               Console.WriteLine($"Deleted {recordCount} lines from the DB.");
            }

            // add new entries
            if (dataByCountry.Any())
            {
               var recordCount = dataByCountry.Count();
               db.AddRange(dataByCountry.ToList());              
               db.SaveChanges();
               Console.WriteLine($"Added {recordCount} lines to the DB.");
            }

         }

      }

      // check if user ask for help
      private static bool HelpRequired(string param)
      {
         var x = param.IndexOf("?", System.StringComparison.Ordinal) + 1;
         x += param.IndexOf("help", System.StringComparison.Ordinal) + 1;

         return x > 0;
      }

      // give user instructions of what is expected
      private static void PrintInstructions()
      {
         Console.WriteLine("READ ME");
         Console.WriteLine("Expected Import Format is a JSON FILE (filename.json)");
         Console.WriteLine("All Properties are Case Sensitive");
         // Console.WriteLine("Expected JSON property: Id (Primary Key)");
         Console.WriteLine("Expected JSON property: CountryCode (iso Country Code)");
         Console.WriteLine("Expected JSON property: Code (postal code)");
         Console.WriteLine("Expected JSON property: StateCode (state code)");
         Console.WriteLine("Expected JSON property: StateName (state name)");
         Console.WriteLine("Expected JSON property: CountyCode (county/province code)");
         Console.WriteLine("Expected JSON property: CountyName (county/province name)");
         Console.WriteLine("Optional JSON property: CommunityCode (community code)");
         Console.WriteLine("Optional JSON property: CommunityName (community name)");
         Console.WriteLine("Optional JSON property: Latitude (in decimal)");
         Console.WriteLine("Optional JSON property: Longitude (in decimal)");
         Console.WriteLine("Optional JSON property: accuracy (accuracy of lat/lng)");

         Console.WriteLine(); // space

         Console.WriteLine("If asked a question you need help with, reply with '?'");

      }

   }
}
