using System;
using System.Linq;

namespace CalcClass
{
    public enum CalcSystems
    {
        SystemBin,
        SystemOct,
        SystemDec,
        SystemHex
    }
    
    public class SystemsFunctions
    {
        public bool checkSystemChar(CalcSystems calcDTypes, char value)
        {
            char[] validChars;
            
            switch (calcDTypes)
            {
                case CalcSystems.SystemBin:
                    validChars = new[] {'0', '1'};
                    return validChars.Contains(char.ToLower(value));
                case CalcSystems.SystemDec:
                    validChars = new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
                    return validChars.Contains(char.ToLower(value));
                case CalcSystems.SystemOct:
                    validChars = new[] {'0', '1', '2', '3', '4', '5', '6', '7'};
                    return validChars.Contains(char.ToLower(value));
                case CalcSystems.SystemHex:
                    validChars = new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
                    return validChars.Contains(char.ToLower(value));
            }

            return false;
        }

        public int getMultiplyer(CalcSystems calcDTypes)
        {
            switch (calcDTypes)
            {
                case CalcSystems.SystemBin:
                    return 2;
                case CalcSystems.SystemDec:
                    return 10;
                case CalcSystems.SystemOct:
                    return 8;
                case CalcSystems.SystemHex:
                    return 16;
                default:
                    return 0;
            }
        }

        public int calculateLetterToNumber(char letter)
        {
            return char.ToLower(letter) - 87;
        }

        public char calculateNumberToChar(long number)
        {
            return (char)(number + 87);
        }

        public string convertToSystem(long value, CalcSystems calcSystems)
        {
            string output;
            switch (calcSystems)
            {
                case CalcSystems.SystemBin:
                    output = "";

                    while (value > 0)
                    {
                        output = (value % 2) + output;
                        value = value / 2;
                    }
                    
                    return output;
                    break;
                case CalcSystems.SystemDec:
                    return value.ToString();
                    break;
                case CalcSystems.SystemHex:
                    output = "";

                    while (value > 0)
                    {
                        if (value % 16 <= 9)
                        {
                            output = (value % 16) + output;
                        }
                        else
                        {
                            output = calculateNumberToChar(value % 16) + output;
                        }
                        value = value / 16;
                    }
                    
                    return output;
                case CalcSystems.SystemOct:
                    output = "";

                    while (value > 0)
                    {
                        output = (value % 8) + output;
                        value = value / 8;
                    }
                    
                    return output;
                    break;
                default:
                    return "";
            }
        }
    }
}