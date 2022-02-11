using System;
using System.Collections.Generic;

namespace Binary_to_Decimal_Converter.Services
{
    public class ConverterService
    {
        public ConverterService()
        {
        }

        public static Dictionary<int, char> DecToHex { get; } = new Dictionary<int, char>()
        {
            {0,'0'}, {1,'1'}, {2,'2'}, {3,'3'}, {4,'4'}, {5,'5'}, {6,'6'}, {7,'7'},
            {8,'8'}, {9,'9'}, {10,'a'}, {11,'b'}, {12,'c'}, {13,'d'}, {14,'e'}, {15,'f'}
        };

        public static Dictionary<char, int> HexToDec { get; } = new Dictionary<char, int>()
        {
            {'0',0}, {'1',1}, {'2',2}, {'3',3}, {'4',4}, {'5',5}, {'6',6}, {'7',7},
            {'8',8}, {'9',9}, {'a',10}, {'b',11}, {'c',12}, {'d', 13}, {'e', 14}, {'f', 15}
        };

        public static Dictionary<string, char> BinToHex { get; } = new Dictionary<string, char>()
        {
            {"0000",'0'}, {"0001",'1'}, {"0010",'2'}, {"0011",'3'}, {"0100",'4'}, {"0101",'5'}, {"0110",'6'}, {"0111",'7'},
            {"1000",'8'}, {"1001",'9'}, {"1010",'a'}, {"1011",'b'}, {"1100",'c'}, {"1101",'d'}, {"1110",'e'}, {"1111",'f'}
        };

        public static string HeaderFrom { get; private set; } = "Binary";

        public static string HeaderTo { get; private set; } = "Decimal";

        public static string ConvertFrom { get; private set; } = "Binary";

        public static string ConvertTo { get; private set; } = "Decimal";

        public static string ConversionVal { get; private set; }

        public static string ConvertedVal { get; private set; }

        public static void SetConversion(string ConvertTypeFrom, string ConvertTypeTo)
        {
            ConvertFrom = ConvertTypeFrom;
            ConvertTo = ConvertTypeTo;
        }

        public static void SetHeader(string ConvertTypeFrom, string ConvertTypeTo)
        {
            HeaderFrom = ConvertTypeFrom;
            HeaderTo = ConvertTypeTo;
        }

        public static void SetConversionVal(string val) {
            ConversionVal = val;
        }

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
    }
}
