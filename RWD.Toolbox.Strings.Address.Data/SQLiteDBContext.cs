

using Microsoft.EntityFrameworkCore;
using System.IO;

namespace RWD.Toolbox.Strings.Address.Data
{
   public class SQLiteDBContext : DbContext
   {
      private readonly string _connectionString = "Data Source=postalData.db";

      /// <summary>
      /// 
      /// </summary>
      public SQLiteDBContext() : this(null) { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="connectionString"></param>
      public SQLiteDBContext(string connectionString)
      {
         if (!string.IsNullOrWhiteSpace(connectionString))
            _connectionString = connectionString;
        Database.EnsureCreated();
      }

      /// <summary>
      /// Collection of World Postal Codes and Supporting Data
      /// </summary>
      public DbSet<PostalCode> PostalCodes { get; set; }
      protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite(_connectionString);

   }
}
