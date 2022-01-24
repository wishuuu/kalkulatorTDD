using System;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            long value = (long) Math.Pow(2, 8) - 1;
            string binary = Convert.ToString(value, 2);
            Console.WriteLine(value);
            Console.WriteLine(binary);

            value = (sbyte) value;
            Console.WriteLine(value);
            


        }
    }
}