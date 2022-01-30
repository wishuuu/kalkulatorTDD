using System;

namespace CalcClassProject
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
                case CalcDTypes.TypByte8: return (long)Math.Pow(2, 8)-1;
                case CalcDTypes.TypeWWord16: return (long)Math.Pow(2, 16)-1;
                case CalcDTypes.TypeDWord32: return (long)Math.Pow(2, 32)-1;
                case CalcDTypes.TypeQWord64: return Int64.MaxValue | Int64.MinValue;
                default: return 0;
            }
        }

        public long convertDecToType(CalcDTypes dType, long value)
        {
            switch (dType)
            {
                case CalcDTypes.TypByte8: return (sbyte)value;
                case CalcDTypes.TypeWWord16: return (Int16)value;
                case CalcDTypes.TypeDWord32: return (Int32)value;
                case CalcDTypes.TypeQWord64: return (Int64)value;
                default: return value;
            }
        }
    }
}