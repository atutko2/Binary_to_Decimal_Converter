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

        internal void convert()
        {

            ConverterService.SetConversionVal(ValueToConvert);

            // if the value being converted is binary
            if ( ConverterService.GetConversionFrom() == "Binary")
            {
                BigInteger conversion = new BigInteger(0);
                // if the binary is being converted to decimal
                if (ConverterService.GetConversionTo() == "Decimal")
                {
                    for (int i = 0; i < ValueToConvert.Length; i++)
                    {
                        conversion *= 2;
                        if (ValueToConvert[i] == '1')
                            conversion++;
                    }
                }

                ConverterService.SetConvertedVal(conversion.ToString());
            }
            // if the value being converted is decimal
            else if( ConverterService.GetConversionFrom() == "Decimal" )
            {
                StringBuilder builder = new StringBuilder();
                BigInteger val = BigInteger.Parse(ValueToConvert);

                if(val == 0)
                {
                    ConverterService.SetConvertedVal("0");
                    return;
                }
                // if the decimal is being converted to binary
                if (ConverterService.GetConversionTo() == "Binary")
                {
                    List<int> bin = new List<int>();
                    
                    while (val > 0)
                    {
                        bin.Insert(0, (int)(val % 2));
                        val /= 2;
                    }

                    foreach (int num in bin)
                    {
                        builder.Append(num);
                    }
                }
                else if( ConverterService.GetConversionTo() == "Hexadecimal")
                {
                    List<char> hex = new List<char>();

                    while (val > 0)
                    {
                        hex.Insert(0, ConverterService.GetHexVal( (int)(val % 16)) );
                        val /= 16;

                    }

                    foreach (char character in hex)
                    {
                        builder.Append(character);
                    }
                }

                ConverterService.SetConvertedVal(builder.ToString());
                
            }
        }

    }
}

public enum Options { Binary, Decimal }