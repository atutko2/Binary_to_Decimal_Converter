/*
 * Author: Adam Tutko
 * Date: 02/14/2022
 * Description: Defines the razor page backend taking a value to convert from one base to another
 */

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

        // defines the converter need to change base and binds it to the input value
        [BindProperty]
        public Converter NewConverter { get; set; }

        // defines the value to convert from and binds it to the input value
        [BindProperty]
        public string ConvertFrom { get; set; }

        // defines the value to convert to and binds it the input value
        [BindProperty]
        public string ConvertTo { get; set; }

        // defines what the header on the razor pages says (e.g. "" to Decimal)
        public string HeaderFrom { get; set; }

        // defines what the header on the razor pages says (e.g. Binary to "")
        public string HeaderTo { get; set; }

        public string ReturnVal { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            
            _logger = logger;
            NewConverter = new Converter();

        }

        public IActionResult OnGet()
        {
            // reset the values on the page then return the page
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
            
        }

        // when one of the select values is changed
        public IActionResult OnPostSelect()
        {

            // saves the conversion values
            ConverterService.SetConversion(ConvertFrom, ConvertTo);

            // if the two values are not the same, change the headers on the page
            if (ConvertFrom != ConvertTo)
                ConverterService.SetHeader(ConvertFrom, ConvertTo);

            // reset the page with the new values
            return RedirectToAction("Get");
        }

        // when the convert button is pressed
        public IActionResult OnPostConvert()
        {

            // check to make sure the conversion values are legal (e.g. if binary must be 1s and 0s)
            ErrorCheck();

            // if any of the input was bad, return the page with the errors
            if (!ModelState.IsValid)
            {
                ConvertFrom = ConverterService.GetConversionFrom();
                ConvertTo = ConverterService.GetConversionTo();
                HeaderFrom = ConverterService.GetHeaderFrom();
                HeaderTo = ConverterService.GetHeaderTo();
                return Page();
            }

            // if it is valid input convert to the new base
            NewConverter.convert();

            // if the page is valid, clear any previous errors that may have been set
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            // reset the values on the page to the newly calculated values
            return RedirectToAction("Get");
        }

        // check the input for validity
        public void ErrorCheck()
        {
            // if the input is empty return a blank error
            if (NewConverter.ValueToConvert == null || NewConverter.ValueToConvert.Length <= 0)
            {
                ModelState.AddModelError("ValueToConvert", "");
                return;
            }

            // if the conversion is from binary check to make sure input is only 1s and 0s
            if (ConverterService.GetConversionFrom() == "Binary")
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

            // if the conversion is from decimal check for only numbers between 0 and 9
            else if(ConverterService.GetConversionFrom() == "Decimal")
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
            // if the conversion is from hexadecimal check for numbers between 0 and 9 and letters between a and f
            else if (ConverterService.GetConversionFrom() == "Hexadecimal")
            {
                for (int i = 0; i < NewConverter.ValueToConvert.Length; i++)
                {
                    // check the input to make sure it is just hex digits
                    if ( !(NewConverter.ValueToConvert[i] >= '0' && NewConverter.ValueToConvert[i] <= '9') && !(NewConverter.ValueToConvert[i] >= 'a' && NewConverter.ValueToConvert[i] <= 'f'))
                    {
                        ModelState.AddModelError("ValueToConvert", "Value must only contain hex digits.");
                        break;
                    }
                }
            }


        }

    }
}