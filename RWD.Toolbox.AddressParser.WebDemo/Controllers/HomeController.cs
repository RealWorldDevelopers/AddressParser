using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RWD.Toolbox.Strings.Address.Demo.Models;
using RWD.Toolbox.Strings.Address;
using System;
using System.Diagnostics;
using System.Text;

namespace RWD.Toolbox.AddressParser.WebDemo.Controllers
{
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;

      private readonly IParser _addressParser;
      private readonly IFormatter _addressFormatter;
      private readonly IRegExHelper _regExHelper;

      public HomeController(ILogger<HomeController> logger)
      {
         // TODO use DI
         _logger = logger;
         _addressParser = new Parser();
         _addressFormatter = new Formatter();
         _regExHelper = new RegExHelper();
      }

      public IActionResult Index()
      {
         var model = new IndexViewModel();
         model.InputString = "7768 s anderson ave ne apartment   2   PO Box 99 warren  ohio 44484-9987 usa";
         return View(model);
      }

      [HttpPost]
      public IActionResult Parse(IndexViewModel model)
      {
         ModelState.Clear();
         model.Result = UpdateParseResultDisplay(model.InputString);
         return View("Index", model);
      }

      public IActionResult Patterns()
      {
         var model = new PatternsViewModel
         {
            // Display Patterns
            AddressPattern = _regExHelper.AddressPattern,
            AllSecondaryUnitPattern = _regExHelper.AllSecondaryUnitPattern,
            CityAndStatePattern = _regExHelper.CityAndStatePattern,
            CountryPattern = _regExHelper.CountryPattern,
            DirectionalPattern = _regExHelper.DirectionalPattern,
            DirectionStreetPattern = _regExHelper.DirectionStreetPattern,
            NumberPattern = _regExHelper.NumberPattern,
            PlacePattern = _regExHelper.PlacePattern,
            POBoxPattern = _regExHelper.POBoxPattern,
            RangedSecondaryUnitPattern = _regExHelper.RangedSecondaryUnitPattern,
            RangelessSecondaryUnitPattern = _regExHelper.RangelessSecondaryUnitPattern,
            StatePattern = _regExHelper.StatePattern,
            StreetPattern = _regExHelper.StreetPattern,
            SuffixPattern = _regExHelper.SuffixPattern,
            PostalCodePattern = _regExHelper.PostalCodePattern
         };

         return View(model);
      }


      public string UpdateParseResultDisplay(string address)
      {
         var sb = new StringBuilder();

         var dto = _addressParser.Parse(address);

         // TODO async and thread

         // TODO compare patterns this first


         // display block
         var full = "FullAddress: " + _addressFormatter.BuildSingleLine(dto);
         sb.AppendLine(full);

         // blank lines for space
         sb.AppendLine();

         var streetLine = "StreetLine: " + _addressFormatter.BuildStreetLine(dto);
         sb.AppendLine(streetLine);
         var number = "Number: " + dto.Number;
         sb.AppendLine(number);
         var pre = "PreDirectional: " + dto.PreDirectional;
         sb.AppendLine(pre);
         var street = "Street: " + dto.Street;
         sb.AppendLine(street);
         var suffix = "Suffix: " + dto.Suffix;
         sb.AppendLine(suffix);
         var post = "Postdirectional: " + dto.PostDirectional;
         sb.AppendLine(post);
         var unit2 = "SecondaryUnit: " + dto.SecondaryUnit;
         sb.AppendLine(unit2);
         var numb2 = "SecondaryNumber: " + dto.SecondaryNumber;
         sb.AppendLine(numb2);

         sb.AppendLine();

         var poBox = "PO Box: " + dto.POBoxNumber;
         sb.AppendLine(poBox);

         sb.AppendLine();

         var city = "City: " + dto.City;
         sb.AppendLine(city);
         var county = "County: " + dto.County;
         sb.AppendLine(county);
         var state = "State: " + dto.State;
         sb.AppendLine(state);
         var zip = "ZipPlus4: " + dto.ZipPlus4;
         sb.AppendLine(zip);
         var country = "Country: " + dto.Country;
         sb.AppendLine(country);

         sb.AppendLine();

         var multiLine = "MultiLine: " + Environment.NewLine + _addressFormatter.BuildMultiline(dto);
         sb.AppendLine(multiLine);


         return sb.ToString();

      }






      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }

   }
}
