
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RWD.Toolbox.Strings.Address.Common.DTO;


namespace RWD.Toolbox.Strings.Address.Test
{
   [TestClass()]
   public class ParserTest
   {
      // Private Shared _masterCodeSet As IMasterCodeSet
      private readonly IParser _addressParser;
      private readonly IFormatter _addressFormatter;
      private readonly IMasterCodeSet _masterCodeSet;
      private readonly IRegExHelper _regExHelper;

      public ParserTest()
      {
         _masterCodeSet = new MasterCodeSet();
         _regExHelper = new RegExHelper(_masterCodeSet);
         _addressParser = new Parser(_regExHelper, _masterCodeSet);
         _addressFormatter = new Formatter();
      }

      // double spaces are on purpose for testing 
      // address dto is never nothing

      [TestMethod()]
      public void Testing_Returns_FullAddress_As_Typed_If_Failed_To_Parse()
      {
         var dto = _addressParser.Parse("zz123456asf;ldaskjfoinlneeeDDDfsal; f2j");
         Assert.IsNotNull(dto);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      private bool _testIgnoreCase = true;

      [TestMethod()]
      public void Testing_SingleLine_Without_Punctuation()
      {
         var dto = _addressParser.Parse("7768 E ST Anderson  STREET ne apartment 12A PO BOX 99 warren Oh 44484-1997 usa");
         Assert.IsNotNull(dto);
         Assert.AreEqual("7768 E ST ANDERSON ST NE APT 12A; PO BOX 99; WARREN OH  44484-1997", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("7768", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("ST Anderson", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("NE", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("12A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.AreEqual("99", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484-1997", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_FullAddress_With_Punctuation()
      {
         var dto = _addressParser.Parse("7768 E. Anderson  Avenue n.e. apartment A2 P.O. BOX 99 warren, Oh. 44484-1997 u.s.a.");
         Assert.IsNotNull(dto);
         Assert.AreEqual("7768 E ANDERSON AVE NE APT A2; PO BOX 99; WARREN OH  44484-1997", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("7768", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("ANDERSON", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("NE", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("A2", dto.SecondaryNumber, _testIgnoreCase);
         Assert.AreEqual("99", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484-1997", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_FullAddress_With_Punctuation_With_Whitespace()
      {
         var dto = _addressParser.Parse("  7768 E. Anderson  Avenue n.e. apartment 2 P.O. BOX 99 warren, Oh. 44484-1997 u.s.a.  ");
         Assert.IsNotNull(dto);
         Assert.AreEqual("7768 E ANDERSON AVE NE APT 2; PO BOX 99; WARREN OH  44484-1997", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("7768", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("ANDERSON", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("NE", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("2", dto.SecondaryNumber, _testIgnoreCase);
         Assert.AreEqual("99", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484-1997", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Typicaldto_Without_Punctuation()
      {
         var dto = _addressParser.Parse("1005 N Gravenstein Highway Sebastopol CA 95472");
         Assert.IsNotNull(dto);
         Assert.AreEqual("1005 N GRAVENSTEIN HWY; SEBASTOPOL CA  95472", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("1005", dto.Number, _testIgnoreCase);
         Assert.AreEqual("N", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("GRAVENSTEIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("HWY", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("SEBASTOPOL", dto.City, _testIgnoreCase);
         Assert.AreEqual("CA", dto.State, _testIgnoreCase);
         Assert.AreEqual("95472", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Typicaldto_With_Punctuation()
      {
         var dto = _addressParser.Parse("1005 N. Gravenstein Highway, Sebastopol, CA. 95472");
         Assert.IsNotNull(dto);
         Assert.AreEqual("1005 N GRAVENSTEIN HWY; SEBASTOPOL CA  95472", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("1005", dto.Number, _testIgnoreCase);
         Assert.AreEqual("N", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("GRAVENSTEIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("HWY", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("SEBASTOPOL", dto.City, _testIgnoreCase);
         Assert.AreEqual("CA", dto.State, _testIgnoreCase);
         Assert.AreEqual("95472", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Typicaldto_With_Punctuation_NoZip()
      {
         var dto = _addressParser.Parse("1005 N. Gravenstein Highway, Sebastopol, CA.");
         Assert.IsNotNull(dto);
         Assert.AreEqual("1005 N GRAVENSTEIN HWY; SEBASTOPOL CA", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("1005", dto.Number, _testIgnoreCase);
         Assert.AreEqual("N", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("GRAVENSTEIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("HWY", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("SEBASTOPOL", dto.City, _testIgnoreCase);
         Assert.AreEqual("CA", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testingdto_WithRangeless_SecondaryUnit()
      {
         var dto = _addressParser.Parse("1050 Broadway Penthouse, New York, NY 10001");
         Assert.IsNotNull(dto);
         Assert.AreEqual("1050 BROADWAY PH; NEW YORK NY  10001", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("1050", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("BROADWAY", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("PH", dto.SecondaryUnit, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("NEW YORK", dto.City, _testIgnoreCase);
         Assert.AreEqual("NY", dto.State, _testIgnoreCase);
         Assert.AreEqual("10001", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testingdto_WithSpaced_Alphanumeric_Range()
      {
         var dto = _addressParser.Parse("N645 W23001 BLUEMOUND ROAD, ARLINGTON HEIGHTS, IL, 60006");
         Assert.IsNotNull(dto);
         Assert.AreEqual("N645W23001 BLUEMOUND RD; ARLINGTON HEIGHTS IL  60006", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("N645W23001", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("BLUEMOUND", dto.Street, _testIgnoreCase);
         Assert.AreEqual("RD", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("ARLINGTON HEIGHTS", dto.City, _testIgnoreCase);
         Assert.AreEqual("IL", dto.State, _testIgnoreCase);
         Assert.AreEqual("60006", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Queens_NYdto()
      {
         var dto = _addressParser.Parse("123-465 34th St New York NY 10024");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123-465 34TH ST; NEW YORK NY  10024", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123-465", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("34TH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("NEW YORK", dto.City, _testIgnoreCase);
         Assert.AreEqual("NY", dto.State, _testIgnoreCase);
         Assert.AreEqual("10024", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Cardinal_Street_1()
      {
         var dto = _addressParser.Parse("500 SOUTH STREET SE VIRGINIA BEACH VIRGINIA 23452");
         Assert.IsNotNull(dto);
         Assert.AreEqual("500 SOUTH ST SE; VIRGINIA BEACH VA  23452", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("500", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("SOUTH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.AreEqual("SE", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("VIRGINIA BEACH", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23452", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Cardinal_Street_2()
      {
         var dto = _addressParser.Parse("500 SOUTH STREET SE PENTHOUSE, VIRGINIA BEACH VIRGINIA 23452");
         Assert.IsNotNull(dto);
         Assert.AreEqual("500 SOUTH ST SE PH; VIRGINIA BEACH VA  23452", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("500", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("SOUTH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.AreEqual("SE", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("PH", dto.SecondaryUnit, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("VIRGINIA BEACH", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23452", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Cardinal_Street_3()
      {
         var dto = _addressParser.Parse("500 SOUTH STREET SE PENTHOUSE 5 VIRGINIA BEACH VIRGINIA 23452");
         Assert.IsNotNull(dto);
         Assert.AreEqual("500 SOUTH ST SE PH 5; VIRGINIA BEACH VA  23452", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("500", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("SOUTH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.AreEqual("SE", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("PH", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("5", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("VIRGINIA BEACH", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23452", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Cardinal_Street_4()
      {
         var dto = _addressParser.Parse("500 SOUTH STREET PENTHOUSE, VIRGINIA BEACH VIRGINIA 23452");
         Assert.IsNotNull(dto);
         Assert.AreEqual("500 SOUTH ST PH; VIRGINIA BEACH VA  23452", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("500", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("SOUTH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("PH", dto.SecondaryUnit, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("VIRGINIA BEACH", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23452", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Cardinal_Street_5()
      {
         var dto = _addressParser.Parse("500 SOUTH STREET PENTHOUSE 5 VIRGINIA BEACH VIRGINIA 23452");
         Assert.IsNotNull(dto);
         Assert.AreEqual("500 SOUTH ST PH 5; VIRGINIA BEACH VA  23452", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("500", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("SOUTH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("PH", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("5", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("VIRGINIA BEACH", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23452", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_NumberFraction_Street_SuffixAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 1/2 MAIN ST RICHMOND VA 23221");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 1/2 MAIN ST; RICHMOND VA  23221", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123 1/2", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("RICHMOND", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23221", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Fulldto_Without_Punctuation()
      {
         var dto = _addressParser.Parse("999 West 89th Street Apt A New York NY 10024");
         Assert.IsNotNull(dto);
         Assert.AreEqual("999 W 89TH ST APT A; NEW YORK NY  10024", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("999", dto.Number, _testIgnoreCase);
         Assert.AreEqual("W", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("89TH", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("NEW YORK", dto.City, _testIgnoreCase);
         Assert.AreEqual("NY", dto.State, _testIgnoreCase);
         Assert.AreEqual("10024", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testingdto_With_Alphanumeric_House_Number()
      {
         var dto = _addressParser.Parse("N6W23001 BLUEMOUND ROAD, ARLINGTON HEIGHTS, IL, 60006");
         Assert.IsNotNull(dto);
         Assert.AreEqual("N6W23001 BLUEMOUND RD; ARLINGTON HEIGHTS IL  60006", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("N6W23001", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("BLUEMOUND", dto.Street, _testIgnoreCase);
         Assert.AreEqual("RD", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("ARLINGTON HEIGHTS", dto.City, _testIgnoreCase);
         Assert.AreEqual("IL", dto.State, _testIgnoreCase);
         Assert.AreEqual("60006", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_Country_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OH 44047-1447; UNITED STATES");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_US_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OH 44047-1447; US");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_USA_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OH 44047-1447; USA");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_Country_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST JEFFERSON OH 44047-1447 UNITED STATES");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_US_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST JEFFERSON OH 44047-1447 US");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("UNITED STATES", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_UpperCase_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OH 44047-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_LowerCase_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 main st; jefferson oh 44047-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Zip4_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST JEFFERSON OH 44047-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_City_StateAbrv_Zip4_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN; JEFFERSON OH 44047-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_STREET_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN STREET; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_STREET_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN STREET JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_STR_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN STR; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_STR_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN STR JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_ST_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_ST_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_ST_City_State_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OHIO 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Street_SuffixAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("MAIN ST; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Street_SuffixAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("MAIN ST JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("MAIN ST; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; JEFFERSON OH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_City_StateAbrv_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST JEFFERSON OH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; JEFFERSON OH", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters_LowerCase()
      {
         var dto = _addressParser.Parse("123 e main st s; jefferson oh 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN ST S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MAIN ST S JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN ST S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MAIN ST S JEFFERSON OH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN ST S; JEFFERSON OH", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("E MAIN ST S; JEFFERSON, OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MAIN ST S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("E MAIN ST S JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MAIN ST S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MAIN S; JEFFERSON, OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters_With_Punctuation()
      {
         var dto = _addressParser.Parse("123 N.W. MAIN ST S.W.; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 NW MAIN ST SW; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("NW", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_StreetMultiName_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MARTIN LUTHER KING JR AVE S; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR AVE S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_StreetMultiName_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MARTIN LUTHER KING JR AVE S JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR AVE S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_StreetMultiName_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("E MARTIN LUTHER KING JR AVE S; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MARTIN LUTHER KING JR AVE S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_StreetMultiName_SuffixAbrv_PostDirectAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("E MARTIN LUTHER KING JR AVE S JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MARTIN LUTHER KING JR AVE S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_StreetMultiName_PostDirectAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MARTIN LUTHER KING JR S; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_StreetMultiName_PostDirectAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 E MARTIN LUTHER KING JR S JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirect_StreetMultiName_Suffix_PostDirect_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 EAST MARTIN LUTHER KING JR AVENUE SOUTH; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR AVE S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirect_StreetMultiName_Suffix_PostDirect_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 EAST MARTIN LUTHER KING JR AVENUE SOUTH JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR AVE S; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_SuffixAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVE; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_SuffixAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVE JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_Suffix_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVENUE; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_Suffix_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVENUE JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_StreetMultiName_SuffixAbrv_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("MARTIN LUTHER KING JR AVE; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("MARTIN LUTHER KING JR AVE; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_StreetMultiName_SuffixAbrv_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("MARTIN LUTHER KING JR AVE JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("MARTIN LUTHER KING JR AVE; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Militarydto_1()
      {
         var dto = _addressParser.Parse("PSC BOX 453, APO AE 99969");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PSC BOX 453; APO AE  99969", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("PSC BOX 453", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("APO", dto.City, _testIgnoreCase);
         Assert.AreEqual("AE", dto.State, _testIgnoreCase);
         Assert.AreEqual("99969", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Militarydto_2()
      {
         var dto = _addressParser.Parse("PSC 4 BOX 123; APO AE 09021");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PSC 4 BOX 123; APO AE  09021", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("PSC 4 BOX 123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("APO", dto.City, _testIgnoreCase);
         Assert.AreEqual("AE", dto.State, _testIgnoreCase);
         Assert.AreEqual("09021", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Militarydto_3()
      {
         var dto = _addressParser.Parse("UNIT 123 BOX 456; FPO AP 096691");
         Assert.IsNotNull(dto);
         Assert.AreEqual("UNIT 123 BOX 456; FPO AP  096691", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("UNIT 123 BOX 456", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("FPO", dto.City, _testIgnoreCase);
         Assert.AreEqual("AP", dto.State, _testIgnoreCase);
         Assert.AreEqual("096691", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Militarydto_4()
      {
         var dto = _addressParser.Parse("UNIT 123 BOX 456; DPO AE 09498-0048");
         Assert.IsNotNull(dto);
         Assert.AreEqual("UNIT 123 BOX 456; DPO AE  09498-0048", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("UNIT 123 BOX 456", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("DPO", dto.City, _testIgnoreCase);
         Assert.AreEqual("AE", dto.State, _testIgnoreCase);
         Assert.AreEqual("09498-0048", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_With_Punctuation()
      {
         var dto = _addressParser.Parse("P.O. BOX 4857, New York, NY 10001");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 4857; New York NY  10001", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("4857", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("NEW YORK", dto.City, _testIgnoreCase);
         Assert.AreEqual("NY", dto.State, _testIgnoreCase);
         Assert.AreEqual("10001", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_Without_Punctuation()
      {
         var dto = _addressParser.Parse("P.O. BOX 4857 New York NY 10001");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 4857; New York NY  10001", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("4857", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("NEW YORK", dto.City, _testIgnoreCase);
         Assert.AreEqual("NY", dto.State, _testIgnoreCase);
         Assert.AreEqual("10001", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_With_Delimiters_With_Comma()
      {
         var dto = _addressParser.Parse("PO BOX 456; JEFFERSON, OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_With_Delimiters_Without_Punctuation()
      {
         var dto = _addressParser.Parse("PO BOX 456; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_With_Delimiters_Without_Punctuation_LoweCase()
      {
         var dto = _addressParser.Parse("po box 456; jefferson oh 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_Without_Delimiters_With_Punctuation()
      {
         var dto = _addressParser.Parse("P.O. BOX 456 JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("PO BOX 456 JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_City_StateAbrv_Zip_With_Delimiters_With_Punctuation()
      {
         var dto = _addressParser.Parse("P.O. BOX 456; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBoxPound_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("PO BOX #456; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Box_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("BOX 456; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Box_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("BOX 456 JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("PO BOX 456; 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_POBox_Only()
      {
         var dto = _addressParser.Parse("PO BOX 456A");
         Assert.IsNotNull(dto);
         Assert.AreEqual("PO BOX 456A", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456A", dto.POBoxNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_POBox_City_StateAbrv_Zip_With_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST; PO BOX 456; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_POBox_City_StateAbrv_Zip_Without_Delimiters()
      {
         var dto = _addressParser.Parse("123 MAIN ST PO BOX 456 JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_POBox_City_StateAbrv_Zip_With_Delimiters_With_Comma()
      {
         var dto = _addressParser.Parse("123 MAIN ST; PO BOX 456; JEFFERSON, OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_POBox_City_StateAbrv_Zip_Without_Delimiters_With_Comma()
      {
         var dto = _addressParser.Parse("123 MAIN ST PO BOX 456 JEFFERSON, OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; PO BOX 456; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_POBox_Zip_Without_Delimiters_With_Comma()
      {
         var dto = _addressParser.Parse("123 MAIN ST PO BOX 456 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; PO Box 456  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.AreEqual("456", dto.POBoxNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44047", dto.ZipPlus4);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv()
      {
         var dto = _addressParser.Parse("123 MAIN ST");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_Unit()
      {
         var dto = _addressParser.Parse("123 MAIN ST APARTMENT 31");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST APT 31", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("31", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Street_SuffixAbrv()
      {
         var dto = _addressParser.Parse("MAIN ST");
         Assert.IsNotNull(dto);
         Assert.AreEqual("MAIN ST", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_Suffix()
      {
         var dto = _addressParser.Parse("123 MAIN STREET");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_Suffix_Unit()
      {
         var dto = _addressParser.Parse("123 MAIN STREET apt 12A");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST APT 12A", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("12A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_Suffix_PostDirect()
      {
         var dto = _addressParser.Parse("123 MAIN STREET SW");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST SW", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_Suffix_PostDirect_Unit()
      {
         var dto = _addressParser.Parse("123 MAIN STREET SW APT 2");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST SW APT 2", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("2", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street()
      {
         var dto = _addressParser.Parse("123 MAIN");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_Unit()
      {
         var dto = _addressParser.Parse("123 MAIN APT 12A");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN APT 12A", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("12A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Street_With_Whitespace()
      {
         var dto = _addressParser.Parse("  7768 Anderson ave  ");
         Assert.IsNotNull(dto);
         Assert.AreEqual("7768 ANDERSON AVE", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("7768", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("ANDERSON", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Without_Whitespace_With_Delimiter()
      {
         var dto = _addressParser.Parse("CONNEAUT, OH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("CONNEAUT OH", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("CONNEAUT", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Zip_Without_Whitespace_With_Delimiter()
      {
         var dto = _addressParser.Parse("WARREN, OH 44484");
         Assert.IsNotNull(dto);
         Assert.AreEqual("WARREN OH  44484", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Zip4_Without_Whitespace_With_Delimiter()
      {
         var dto = _addressParser.Parse("WARREN, OH 44484-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("WARREN OH  44484-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_With_Whitespace()
      {
         var dto = _addressParser.Parse("  CONNEAUT OH  ");
         Assert.IsNotNull(dto);
         Assert.AreEqual("CONNEAUT OH", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("CONNEAUT", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Zip_With_Whitespace()
      {
         var dto = _addressParser.Parse("  CONNEAUT OH 44030  ");
         Assert.IsNotNull(dto);
         Assert.AreEqual("CONNEAUT OH  44030", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("CONNEAUT", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44030", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Without_Whitespace()
      {
         var dto = _addressParser.Parse("CONNEAUT OH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("CONNEAUT OH", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("CONNEAUT", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Zip_Without_Whitespace()
      {
         var dto = _addressParser.Parse("WARREN OH 44484");
         Assert.IsNotNull(dto);
         Assert.AreEqual("WARREN OH  44484", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_City_StateAbrv_Zip4_Without_Whitespace()
      {
         var dto = _addressParser.Parse("WARREN OH 44484-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("WARREN OH  44484-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("WARREN", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44484-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Zip_Without_Whitespace()
      {
         var dto = _addressParser.Parse("44484");
         Assert.IsNotNull(dto);
         Assert.AreEqual("44484", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44484", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Zip_With_Whitespace()
      {
         var dto = _addressParser.Parse("  44484  ");
         Assert.IsNotNull(dto);
         Assert.AreEqual("44484", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44484", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Zip4_Without_Whitespace()
      {
         var dto = _addressParser.Parse("44484-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("44484-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44484-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Zip4_With_Whitespace()
      {
         var dto = _addressParser.Parse("  44484-1447  ");
         Assert.IsNotNull(dto);
         Assert.AreEqual("44484-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Street));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44484-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("123 E MAIN ST S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN ST S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirect_Street_Suffix_PostDirect()
      {
         var dto = _addressParser.Parse("123 EAST MAIN STREET SOUTH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN ST S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_Street_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("123 E MAIN S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MAIN S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_Street_SuffixAbrv_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("E MAIN ST S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MAIN ST S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_Street_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("E MAIN S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MAIN S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_SuffixAbrv()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVE");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_StreetMultiName_SuffixAbrv()
      {
         var dto = _addressParser.Parse("MARTIN LUTHER KING JR AVE");
         Assert.IsNotNull(dto);
         Assert.AreEqual("MARTIN LUTHER KING JR AVE", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_Suffix()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVENUE");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName_Suffix_PostDirect()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR AVE SW");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR AVE SW", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_StreetMultiName_SuffixAbrv_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("123 E MARTIN LUTHER KING JR AVE S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR AVE S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirect_StreetMultiName_Suffix_PostDirect()
      {
         var dto = _addressParser.Parse("123 EAST MARTIN LUTHER KING JR AVENUE SOUTH");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR AVE S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_PreDirectAbrv_StreetMultiName_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("123 E MARTIN LUTHER KING JR S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 E MARTIN LUTHER KING JR S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_PreDirectAbrv_StreetMultiName_SuffixAbrv_PostDirectAbrv()
      {
         var dto = _addressParser.Parse("E MARTIN LUTHER KING JR AVE S");
         Assert.IsNotNull(dto);
         Assert.AreEqual("E MARTIN LUTHER KING JR AVE S", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.AreEqual("E", dto.PreDirectional, _testIgnoreCase);
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.AreEqual("AVE", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("S", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_StreetMultiName()
      {
         var dto = _addressParser.Parse("123 MARTIN LUTHER KING JR");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MARTIN LUTHER KING JR", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MARTIN LUTHER KING JR", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_Zip_With_Delimiter()
      {
         var dto = _addressParser.Parse("123 MAIN ST; 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Number_Street_SuffixAbrv_Zip_Without_Delimiter()
      {
         var dto = _addressParser.Parse("123 MAIN ST  44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Ranged_Unit_Attached_To_Number_1()
      {
         var dto = _addressParser.Parse("403D BERRYFIELD LANE CHESAPEAKE VA 23324");
         Assert.IsNotNull(dto);
         Assert.AreEqual("403 BERRYFIELD LN APT D; CHESAPEAKE VA  23324", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("403", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("BERRYFIELD", dto.Street, _testIgnoreCase);
         Assert.AreEqual("LN", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("D", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("CHESAPEAKE", dto.City, _testIgnoreCase);
         Assert.AreEqual("VA", dto.State, _testIgnoreCase);
         Assert.AreEqual("23324", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Ranged_Unit_Attached_To_Number_2()
      {
         var dto = _addressParser.Parse("123A MAIN ST; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST APT A; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_With_Ranged_Unit_Attached_To_Number_3()
      {
         var dto = _addressParser.Parse("123-1A MAIN ST; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST APT 1A; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("1A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_RR_With_Delimiter()
      {
         var dto = _addressParser.Parse("RR 4 BOX 123; JEFFERSON OH 44047-1447");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RR 4 BOX 123; JEFFERSON OH  44047-1447", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RR 4 BOX 123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047-1447", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_RR_Without_Delimiter()
      {
         var dto = _addressParser.Parse("RR 4 BOX 123 JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RR 4 BOX 123; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RR 4 BOX 123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_RuralRoute_With_Delimiter()
      {
         var dto = _addressParser.Parse("RURAL ROUTE 4 BOX 123; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RURAL ROUTE 4 BOX 123; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RURAL ROUTE 4 BOX 123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Rural_With_Delimiter()
      {
         var dto = _addressParser.Parse("RURAL 4 BOX 123; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RURAL 4 BOX 123; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RURAL 4 BOX 123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_RRPound_With_Delimiter()
      {
         var dto = _addressParser.Parse("RR 4 BOX #123; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RR 4 BOX #123; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RR 4 BOX #123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_RuralRoutePound_With_Delimiter()
      {
         var dto = _addressParser.Parse("RURAL ROUTE 4 BOX #123; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RURAL ROUTE 4 BOX #123; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RURAL ROUTE 4 BOX #123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_RuralPound_With_Delimiter()
      {
         var dto = _addressParser.Parse("RURAL 4 BOX #123; JEFFERSON OH 44047");
         Assert.IsNotNull(dto);
         Assert.AreEqual("RURAL 4 BOX #123; JEFFERSON OH  44047", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Number));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("RURAL 4 BOX #123", dto.Street, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Suffix));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("JEFFERSON", dto.City, _testIgnoreCase);
         Assert.AreEqual("OH", dto.State, _testIgnoreCase);
         Assert.AreEqual("44047", dto.ZipPlus4, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Canadian_1()
      {
         var dto = _addressParser.Parse("10-123 1/2 MAIN ST SE; MONTREAL  QC H3Z 2Y7; CANADA");
         Assert.IsNotNull(dto);
         Assert.AreEqual("10-123 1/2 MAIN ST SE; MONTREAL QC  H3Z 2Y7; CANADA", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("10-123 1/2", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SE", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("MONTREAL", dto.City, _testIgnoreCase);
         Assert.AreEqual("QC", dto.State, _testIgnoreCase);
         Assert.AreEqual("H3Z 2Y7", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("CANADA", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Canadian_2()
      {
         var dto = _addressParser.Parse("10-123 1/2 MAIN ST SE MONTREAL  QC H3Z 2Y7 CA");
         Assert.IsNotNull(dto);
         Assert.AreEqual("10-123 1/2 MAIN ST SE; MONTREAL QC  H3Z 2Y7; CANADA", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("10-123 1/2", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SE", dto.PostDirectional, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("MONTREAL", dto.City, _testIgnoreCase);
         Assert.AreEqual("QC", dto.State, _testIgnoreCase);
         Assert.AreEqual("H3Z 2Y7", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("CANADA", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Canadian_3()
      {
         var dto = _addressParser.Parse("123 MAIN ST; MILLARVILLE AB  T0L 1K0; CANADA");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; MILLARVILLE AB  T0L 1K0; CANADA", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("MILLARVILLE", dto.City, _testIgnoreCase);
         Assert.AreEqual("AB", dto.State, _testIgnoreCase);
         Assert.AreEqual("T0L 1K0", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("CANADA", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Canadian_4()
      {
         var dto = _addressParser.Parse("123 MAIN ST MILLARVILLE AB  T0L 1K0 CANADA");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST; MILLARVILLE AB  T0L 1K0; CANADA", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PostDirectional));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryUnit));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.SecondaryNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.AreEqual("MILLARVILLE", dto.City, _testIgnoreCase);
         Assert.AreEqual("AB", dto.State, _testIgnoreCase);
         Assert.AreEqual("T0L 1K0", dto.ZipPlus4, _testIgnoreCase);
         Assert.AreEqual("CANADA", dto.Country, _testIgnoreCase);
      }

      [TestMethod()]
      public void Testing_Secondary_Unit_Numbers_1()
      {
         var dto = _addressParser.Parse("123 MAIN STREET SW APT 2");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST SW APT 2", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("2", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Secondary_Unit_Numbers_2()
      {
         var dto = _addressParser.Parse("123 MAIN STREET SW APT 12A");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST SW APT 12A", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("12A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Secondary_Unit_Numbers_3()
      {
         var dto = _addressParser.Parse("123 MAIN STREET SW # 12A");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST SW APT 12A", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("12A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));
      }

      [TestMethod()]
      public void Testing_Secondary_Unit_Numbers_4()
      {
         var dto = _addressParser.Parse("123 MAIN STREET SW #12A");
         Assert.IsNotNull(dto);
         Assert.AreEqual("123 MAIN ST SW APT 12A", _addressFormatter.BuildSingleLine(dto), _testIgnoreCase);
         Assert.AreEqual("123", dto.Number, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.PreDirectional));
         Assert.AreEqual("MAIN", dto.Street, _testIgnoreCase);
         Assert.AreEqual("ST", dto.Suffix, _testIgnoreCase);
         Assert.AreEqual("SW", dto.PostDirectional, _testIgnoreCase);
         Assert.AreEqual("APT", dto.SecondaryUnit, _testIgnoreCase);
         Assert.AreEqual("12A", dto.SecondaryNumber, _testIgnoreCase);
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.POBoxNumber));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.City));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.State));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.ZipPlus4));
         Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Country));

      }
   }
}