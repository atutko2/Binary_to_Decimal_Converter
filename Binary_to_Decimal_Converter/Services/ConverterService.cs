/*
 * Author: Adam Tutko
 * Date: 02/14/2022
 * Description: Defines necessary tooling to convert from one base to another and holds onto the converted values
 */

using System;
using System.Collections.Generic;

namespace Binary_to_Decimal_Converter.Services
{
    public class ConverterService
    {
        public ConverterService()
        {
        }

        // dictionary to convert from Decimal to Hexadecimal
        public static Dictionary<int, char> DecToHex { get; } = new Dictionary<int, char>()
        {
            {0,'0'}, {1,'1'}, {2,'2'}, {3,'3'}, {4,'4'}, {5,'5'}, {6,'6'}, {7,'7'},
            {8,'8'}, {9,'9'}, {10,'a'}, {11,'b'}, {12,'c'}, {13,'d'}, {14,'e'}, {15,'f'}
        };

        // dictionary to convert from Hexadecimal to Decimal
        public static Dictionary<char, int> HexToDec { get; } = new Dictionary<char, int>()
        {
            {'0',0}, {'1',1}, {'2',2}, {'3',3}, {'4',4}, {'5',5}, {'6',6}, {'7',7},
            {'8',8}, {'9',9}, {'a',10}, {'b',11}, {'c',12}, {'d', 13}, {'e', 14}, {'f', 15}
        };

        // dictionary to convert from Binary to Hexadecimal
        public static Dictionary<string, char> BinToHex { get; } = new Dictionary<string, char>()
        {
            {"0000",'0'}, {"0001",'1'}, {"0010",'2'}, {"0011",'3'}, {"0100",'4'}, {"0101",'5'}, {"0110",'6'}, {"0111",'7'},
            {"1000",'8'}, {"1001",'9'}, {"1010",'a'}, {"1011",'b'}, {"1100",'c'}, {"1101",'d'}, {"1110",'e'}, {"1111",'f'}
        };

        // dictionary to convert from Hexadecimal to Binary
        public static Dictionary<char, string> HexToBin { get; } = new Dictionary<char, string>()
        {
            {'0', "0000"}, {'1', "0001"}, {'2', "0010"}, {'3', "0011"}, {'4', "0100"}, {'5', "0101"}, {'6', "0110"}, {'7', "0111"},
            {'8', "1000"}, {'9', "1001"}, {'a', "1010"}, {'b', "1011"}, {'c', "1100"}, {'d', "1101"}, {'e', "1110"}, {'f', "1111"}
        };

        // holds onto the static header from variables to reset
        public static string HeaderFrom { get; private set; } = "Binary";

        // holds onto the static header to variables to reset
        public static string HeaderTo { get; private set; } = "Decimal";

        // holds onto the static convert from variables to reset
        public static string ConvertFrom { get; private set; } = "Binary";

        // holds onto the static convert to variables to reset
        public static string ConvertTo { get; private set; } = "Decimal";

        // holds onto the static value being converted variables to reset
        public static string ConversionVal { get; private set; }

        // holds onto the static value that was converted variables to reset
        public static string ConvertedVal { get; private set; }

        // sets the convert from and to values to the ones passed in
        public static void SetConversion(string ConvertTypeFrom, string ConvertTypeTo)
        {
            ConvertFrom = ConvertTypeFrom;
            ConvertTo = ConvertTypeTo;
        }

        // sets the header from and to values to the ones passed in
        public static void SetHeader(string ConvertTypeFrom, string ConvertTypeTo)
        {
            HeaderFrom = ConvertTypeFrom;
            HeaderTo = ConvertTypeTo;
        }

        // sets the conversion value to the ones passed in
        public static void SetConversionVal(string val) {
            ConversionVal = val;
        }

        // sets the converted value to the ones passed in
        public static void SetConvertedVal(string val)
        {
            ConvertedVal = val;
        }

        public static string GetConversionFrom() {
            return ConvertFrom;
        }

        public static string GetConversionTo()
        {
            return ConvertTo;
        }

        public static string GetHeaderFrom()
        {
            return HeaderFrom;
        }

        public static string GetHeaderTo()
        {
            return HeaderTo;
        }

        public static string GetConversionVal()
        {
            return ConversionVal;
        }

        public static string GetConvertedVal()
        {
            return ConvertedVal;
        }

        public static char GetHexVal( int val)
        {
            return DecToHex[val];
        }

        public static char GetHexVal(string val)
        {
            return BinToHex[val];
        }

        public static int GetDecVal(char val)
        {
            return HexToDec[val];
        }
        public static string GetBinVal(char val)
        {
            return HexToBin[val];
        }
    }
}
