using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CalcClassProject;

namespace Calc
{
    class CLI
    {
        private static ICalcClass _calcClass = new CalcClass();

        static void Main(string[] args)
        {
            string message = "";
            string system = "";
            string wordsize = "";
            while (true)
            {
                Console.WriteLine("Wartość kalkulatora: {0}", _calcClass.calcValuesInActiveSystem);
                Console.WriteLine("Bity:");
                Console.WriteLine("63             48              32              16      8   4    ");
                Console.WriteLine("|              |               |               |       |   |    ");
                Console.WriteLine(string.Join("", _calcClass.binArray.Reverse()));

                switch (_calcClass.CalcSystem)
                {
                    case CalcSystems.SystemBin:
                        system = "bin";
                        break;
                    case CalcSystems.SystemOct:
                        system = "oct";
                        break;
                    case CalcSystems.SystemDec:
                        system = "dec";
                        break;
                    case CalcSystems.SystemHex:
                        system = "hex";
                        break;
                }
                
                Console.WriteLine("Aktualy system: {0}", system);

                switch (_calcClass.CalcDType)
                {
                    case CalcDTypes.TypByte8:
                        wordsize = "8";
                        break;
                    case CalcDTypes.TypeWWord16:
                        wordsize = "16";
                        break;
                    case CalcDTypes.TypeDWord32:
                        wordsize = "32";
                        break;
                    case CalcDTypes.TypeQWord64:
                        wordsize = "64";
                        break;
                }
                
                Console.WriteLine("Aktualy wielkość: {0}", wordsize);

                Console.WriteLine("Dostępne operacje: +, -, *, /, %, p(ower), ^(xor), |(or), &(and), !(not), >, <, =");
                Console.WriteLine("Dostępne komendy:");
                Console.WriteLine("M+ => dodanie do pamięci");
                Console.WriteLine("M- => odjęcie od pamięci");
                Console.WriteLine("MS => zapisanie do pamięci");
                Console.WriteLine("MC => wyczyszczenie pamięci");
                Console.WriteLine("ML => wczytanie z pamięci");
                Console.WriteLine("Swap-{int} => zamiana bitu");
                Console.WriteLine("Rst => restart");
                
                message = Console.ReadLine();

                if (message.Length == 0)
                    continue;
                if (message.Length == 1)
                {
                    _calcClass.Insert(message[0]);
                }
                else if (message[0] == 'M')
                {
                    switch (message[1])
                    {
                        case '+':
                            _calcClass.MemoryAdd();
                            break;
                        case '-':
                            _calcClass.MemorySub();
                            break;
                        case 'S':
                            _calcClass.MemorySave();
                            break;
                        case 'C':
                            _calcClass.MemoryClear();
                            break;
                        case 'L':
                            _calcClass.MemoryLoad();
                            break;
                    }
                }
                else if (message.Split('-')[0] == "Swap")
                {
                    try
                    {
                        int bitNum = Int32.Parse(message.Split('-')[1]);
                        _calcClass.SwapBit(bitNum);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (message == "Rst")
                {
                    _calcClass.Restart();
                }
                else if (message.Split('-')[0] == "System")
                {
                    switch (message.Split('-')[1])
                    {
                        case "bin":
                            _calcClass.CalcSystem = CalcSystems.SystemBin;
                            break;
                        case "oct":
                            _calcClass.CalcSystem = CalcSystems.SystemOct;
                            break;
                        case "dec":
                            _calcClass.CalcSystem = CalcSystems.SystemDec;
                            break;
                        case "hex":
                            _calcClass.CalcSystem = CalcSystems.SystemHex;
                            break;
                    }
                }
                else if (message.Split('-')[0] == "Wordsize")
                {
                    switch (message.Split('-')[1])
                    {
                        case "8":
                            _calcClass.CalcDType = CalcDTypes.TypByte8;
                            break;
                        case "16":
                            _calcClass.CalcDType = CalcDTypes.TypeWWord16;
                            break;
                        case "32":
                            _calcClass.CalcDType = CalcDTypes.TypeDWord32;
                            break;
                        case "64":
                            _calcClass.CalcDType = CalcDTypes.TypeQWord64;
                            break;
                    }
                }
                Console.Clear();
            }
        }
    }
}