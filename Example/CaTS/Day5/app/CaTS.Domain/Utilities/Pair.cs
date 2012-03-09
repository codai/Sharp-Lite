namespace CaTS.Domain.Utilities
{
    public class Pair<TFirst, TSecond>
    {
        public Pair(TFirst firstValue, TSecond secondValue) {
            First = firstValue;
            Second = secondValue;
        }

        public TFirst First { get; set; }
        public TSecond Second { get; set; }
    }
}
