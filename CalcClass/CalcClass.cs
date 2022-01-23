using System;
using System.Linq;

namespace CalcClass
{
    public class CalcClass
    {
        public long calcValue;
        private long shortMemory;
        private char activeOperation;
        private bool afterOperactionFlag = false;
        private readonly SystemsFunctions _systemsFunctions = new SystemsFunctions();
        private readonly Memory _memory;

        public string calcValuesInActiveSystem {
            get { return this.convertToSystem(calcValue);  }
            set { calcValue = this.convertFromSystem(value); }
        }
        
        public CalcSystems CalcSystem { get; set; }
        public CalcDTypes CalcDType { get; set; }

        public int[] binArray
        {
            get
            {
                int[] array = new int[64];
                long actualValue = calcValue;
                int i = 0;
                if (actualValue < 0)
                {
                    array[63] = 1;
                    actualValue = Int64.MinValue + actualValue;
                }
                while (actualValue > 0)
                {
                    array[i] = actualValue % 2 == 1? 1: 0;
                    actualValue = (actualValue / 2);
                    i++;
                }

                return array;
            }
            set
            {
                calcValue = 0;
                for(int i=value.Length-2; i>=0; i--)
                {
                    calcValue *= 2;
                    calcValue += value[i];
                }

                if (value[63] == 1)
                {
                    calcValue += Int64.MinValue;
                }
            }
        }

        public CalcClass()
        {
            calcValue = 0;
            shortMemory = 0;
            activeOperation = '=';
            CalcSystem = CalcSystems.SystemDec;
            CalcDType = CalcDTypes.TypeQWord64;
            _memory = new Memory();
        }

        public void insert(char letter)
        {
            if (_systemsFunctions.checkSystemChar(this.CalcSystem, letter))
            {
                if (afterOperactionFlag)
                {
                    shortMemory = calcValue;
                    calcValue = 0;
                    afterOperactionFlag = false;
                }
                calcValue *= _systemsFunctions.getMultiplyer(this.CalcSystem);
                try
                {
                    calcValue += Int32.Parse(letter.ToString());
                }
                catch (Exception)
                {
                    calcValue += _systemsFunctions.calculateLetterToNumber(letter);
                }
            }
            else if (checkOperationChar(letter))
            {
                setActiveOperation(letter);
            }

        }

        public void Restart()
        {
            calcValue = 0;
            shortMemory = 0;
            activeOperation = '=';
        }

        private void calculateOperation()
        {
            switch (activeOperation)
            {
                case '+':
                    calcValue += shortMemory;
                    break;
                case '-':
                    calcValue = shortMemory - calcValue;
                    break;
                case '*':
                    calcValue *= shortMemory;
                    break;
                case '/':
                    calcValue = shortMemory / calcValue;
                    break;
                case '%':
                    calcValue = shortMemory % calcValue;
                    break;
                case 'p':
                    calcValue = (long)Math.Pow(shortMemory, calcValue);
                    break;
                case '&':
                    calcValue &= shortMemory;
                    break;
                case '|':
                    calcValue |= shortMemory;
                    break;
                case '^':
                    calcValue ^= shortMemory;
                    break;

            }

            afterOperactionFlag = true;
        }

        private void setActiveOperation(char letter)
        {
            if (letter == '=')
            {
                calculateOperation();
                shortMemory = 0;
                activeOperation = '=';
            }
            else if (letter == '!')
                calcValue = calcValue ^ long.MinValue;
            else if (letter == '<')
                calcValue = calcValue << 1;
            else if (letter == '>')
                calcValue = calcValue >> 1;
            else
            {
                if(activeOperation != '=')
                {
                    if (!afterOperactionFlag) calculateOperation();
                }
                else
                {
                    shortMemory = calcValue;
                    calcValue = 0;
                }
                activeOperation = letter;
            }
        }
        
        private string convertToSystem(long value)
        {
            return _systemsFunctions.convertToSystem(value, CalcSystem);
        }

        private long convertFromSystem(string value)
        {
            return 1;
        }

        private bool checkOperationChar(char value)
        {
            char[] validChars = new char[]{'+','-','*','/','^','%','=','p','&','|','^','!','>','<'}; //p - power
            return validChars.Contains(value);
        }

        public void MemoryAdd()
        {
            _memory.add(this.calcValue);
        }

        public void MemorySub()
        {
            _memory.substract(this.calcValue);
        }

        public void MemorySave()
        {
            _memory.overwrite(this.calcValue);
        }

        public void MemoryClear()
        {
            _memory.clear();
        }

        public void MemoryLoad()
        {
            if (afterOperactionFlag)
                shortMemory = calcValue;
            calcValue = _memory.get();
        }

        public void swapBit(int i)
        {
            calcValue ^= (long) Math.Pow(2, i);
        }
    }
}