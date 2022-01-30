namespace CalcClassProject
{
    public interface ICalcClass
    {
        public string calcValuesInActiveSystem { get; set; }
        public CalcSystems CalcSystem { get; set; }
        public CalcDTypes CalcDType { get; set; }
        public int[] binArray { get; set; }
        public void Insert(char letter);
        public void Restart();
        public void MemoryAdd();
        public void MemorySub();
        public void MemorySave();
        public void MemoryClear();
        public void MemoryLoad();
        public void SwapBit(int i);
    }
}