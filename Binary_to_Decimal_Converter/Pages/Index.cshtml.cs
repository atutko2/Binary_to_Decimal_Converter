using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Binary_to_Decimal_Converter.Models;




namespace Binary_to_Decimal_Converter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Converter NewConverter { get; set; }

        [BindProperty]
        public string ConvertFrom { get; set; }

        public string ConvertTo { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            NewConverter = new Converter();
            ConvertFrom = "Binary";
            ConvertTo = "Decimal";
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostSelect()
        {
            if (ConvertFrom == "Binary")
                ConvertTo = "Decimal";
            else
                ConvertTo = "Binary";

            return Page();
        }

            public IActionResult OnPost()
        {


            if (ConvertFrom == "Binary")
            {
                ErrorCheck(1);
                NewConverter.convert(1);
            }
            else
            {
                ErrorCheck(2);
                NewConverter.convert(2);
            }


            // if any of the input was bad, return the page with the errors
            if (!ModelState.IsValid)
            {
                return Page();
            }


            // if the page is valid, clear any previous errors that may have been set
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            return Page();
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