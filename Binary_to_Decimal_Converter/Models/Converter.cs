using System;
using System.Numerics;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Binary_to_Decimal_Converter.Services;

namespace Binary_to_Decimal_Converter.Models
{
    public class Converter
    {
        public Converter()
        {
        }

        [BindProperty]
        public string ValueToConvert { get; set; }

        internal void convert( int convertType )
        {

            ConverterService.SetConversionVal(ValueToConvert);

            if (convertType == 1)
            {
                BigInteger conversion = new BigInteger(0);

                for (int i = 0; i < ValueToConvert.Length; i++)
                {
                    conversion *= 2;
                    if (ValueToConvert[i] == '1')
                        conversion++;
                }

                ConverterService.SetConvertedVal(conversion.ToString());
            }
            else
            {
                List<int> bin = new List<int>();
                BigInteger val = BigInteger.Parse(ValueToConvert);

                while (val > 0)
                {
                    bin.Insert(0, (int)(val % 2));
                    val /= 2;
                }

                StringBuilder builder = new StringBuilder();
                
                foreach (int num in bin)
                {
                   builder.Append(num);
                }

                ConverterService.SetConvertedVal(builder.ToString());
            }
        }

    }
}

public enum Options { Binary, Decimal }