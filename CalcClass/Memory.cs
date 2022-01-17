namespace CalcClass
{
    public class Memory
    {
        private long _value;

        public void add(long v)
        {
            _value += v;
        }

        public void substract(long v)
        {
            _value -= v;
        }

        public void clear()
        {
            _value = 0;
        }

        public long get()
        {
            return _value;
        }

        public void overwrite(long v)
        {
            _value = v;
        }
    }
}