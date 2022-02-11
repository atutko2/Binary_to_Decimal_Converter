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

                // if the binary is being converted to decimal
                if (ConverterService.GetConversionTo() == "Decimal")
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
                // if the binary is being converted to hexadecimal
                else if (ConverterService.GetConversionTo() == "Hexadecimal")
                {
                    StringBuilder builder = new StringBuilder();
                    StringBuilder tmp = new StringBuilder();
                    StringBuilder val = new StringBuilder(ValueToConvert);
                    List<char> hex = new List<char>();
                    int counter = 0;


                    while(counter < val.Length && val[counter] == '0')
                    {
                        counter++;
                    }

                    val.Remove(0, counter);

                    if (val.Length == 0)
                        val.Append('0');

                    // if the value to convert is not a multiple of 4 in length add 0 to the front until it is
                    if( val.Length % 4 != 0)
                    {
                        while(val.Length % 4 != 0)
                        {
                            val.Insert(0, "0");
                        }
                    }

                    // create the hex conversion using grouping
                    for (int i = 0; i < val.Length; i++)
                    {
                        tmp.Append(val[i]);
                        if (i%4 == 3)
                        {
                            hex.Add(ConverterService.GetHexVal(tmp.ToString()));
                            tmp.Clear();
                        }
                        
                    }

                    foreach (char character in hex)
                    {
                        builder.Append(character);
                    }

                    ConverterService.SetConvertedVal(builder.ToString());
                }
                
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

                    // divide the value by two and put the remainder at the start of bin
                    while (val > 0)
                    {
                        bin.Insert(0, (int)(val % 2));
                        val /= 2;
                    }

                    // append each digit to builder
                    foreach (int num in bin)
                    {
                        builder.Append(num);
                    }
                }
                // if the value being converted is hexadecimal
                else if( ConverterService.GetConversionTo() == "Hexadecimal")
                {
                    List<char> hex = new List<char>();

                    // divide the value by 16 and put the converted value at the start of hex
                    while (val > 0)
                    {
                        hex.Insert(0, ConverterService.GetHexVal( (int)(val % 16)) );
                        val /= 16;

                    }

                    // append each character to builder
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

