using System;

namespace CalcClass
{
    public enum CalcDTypes
    {
        TypByte8,
        TypeWWord16,
        TypeDWord32,
        TypeQWord64
    }

    public class TypesFunctions
    {
        public long getMaxValue(CalcDTypes dType)
        {
            switch (dType)
            {
                case CalcDTypes.TypByte8: return byte.MaxValue;
                case CalcDTypes.TypeWWord16: return Int16.MaxValue;
                case CalcDTypes.TypeDWord32: return Int32.MaxValue;
                case CalcDTypes.TypeQWord64: return Int64.MaxValue;
                default: return 0;
            }
        }
    }
}