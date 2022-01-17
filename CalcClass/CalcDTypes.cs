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
        public long getFullBytes(CalcDTypes dType)
        {
            switch (dType)
            {
                case CalcDTypes.TypByte8: return sbyte.MinValue;
                case CalcDTypes.TypeWWord16: return Int16.MinValue;
                case CalcDTypes.TypeDWord32: return Int32.MinValue;
                case CalcDTypes.TypeQWord64: return Int64.MinValue;
                default: return 0;
            }
        }
    }
}