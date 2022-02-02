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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            NewConverter = new Converter();
            ConvertFrom = "Binary";
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {


            if (ConvertFrom == "Binary")
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

                NewConverter.convert(1);
            }
            else
            {
                for (int i = 0; i < NewConverter.ValueToConvert.Length; i++)
                {
                    // check the input to make sure it is just binary
                    if (NewConverter.ValueToConvert[i] < '0' || NewConverter.ValueToConvert[i] > '9')
                    {
                        ModelState.AddModelError("ValueToConvert", "Value must only contain numbers.");
                        break;
                    }
                }
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

    }
}