using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Binary_to_Decimal_Converter.Models;
using Binary_to_Decimal_Converter.Services;


namespace Binary_to_Decimal_Converter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Converter NewConverter { get; set; }

        [BindProperty]
        public string ConvertFrom { get; set; }

        [BindProperty]
        public string ConvertTo { get; set; }

        public string HeaderFrom { get; set; }

        public string HeaderTo { get; set; }

        public string ReturnVal { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            
            _logger = logger;
            NewConverter = new Converter();

        }

        public IActionResult OnGet()
        {
            ConvertFrom = ConverterService.GetConversionFrom();
            ConvertTo = ConverterService.GetConversionTo();
            HeaderFrom = ConverterService.GetHeaderFrom();
            HeaderTo = ConverterService.GetHeaderTo();
            ReturnVal = ConverterService.GetConvertedVal();
            NewConverter.ValueToConvert = ConverterService.GetConversionVal();
            return Page();
        }

        public void OnPost()
        {
            Console.WriteLine("In Post");
        }

        public IActionResult OnPostSelect()
        {
            
            ConverterService.SetConversion(ConvertFrom, ConvertTo);

            if (ConvertFrom != ConvertTo)
                ConverterService.SetHeader(ConvertFrom, ConvertTo);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostConvert( string type )
        {
            Console.WriteLine("In Convert");

            Console.WriteLine(type);
            if (type == "Binary")
            {
                ErrorCheck(1);
            }
            else if( ConverterService.GetConversionFrom() == "Decimal")
            {
                ErrorCheck(2);

            }


            // if any of the input was bad, return the page with the errors
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Get");
            }

            if(type == "Binary")
                NewConverter.convert(1);
            else
                NewConverter.convert(2);

            // if the page is valid, clear any previous errors that may have been set
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            
            if (type == "Binary")
                ConvertTo = "Decimal";
            else
                ConvertTo = "Binary";

            return RedirectToAction("Get");
        }

        public void ErrorCheck( int type)
        {

            // if the conversion is from binary
            if (type == 1)
            {
                for (int i = 0; i < NewConverter.ValueToConvert.Length; i++)
                {
                    // check the input to make sure it is just binary
                    if (NewConverter.ValueToConvert[i] != '0' && NewConverter.ValueToConvert[i] != '1')
                    {
                        ModelState.AddModelError("ValueToConvert", "Value must only contain binary digits.");
                        break;
                    }
                }
            }
            // if the conversion is from decimal
            else
            {
                for (int i = 0; i < NewConverter.ValueToConvert.Length; i++)
                {
                    // check the input to make sure it is just numbers
                    if (NewConverter.ValueToConvert[i] < '0' || NewConverter.ValueToConvert[i] > '9')
                    {
                        ModelState.AddModelError("ValueToConvert", "Value must only contain numbers.");
                        break;
                    }
                }
            }
        }

    }
}