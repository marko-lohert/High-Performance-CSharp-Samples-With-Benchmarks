namespace HighPerformance.Miscellaneous
{
    public class IsPrefixIncrementFasterThanPostfix
    {
        [Benchmark(Baseline = true)]
        public void PostfixIncrement()
        {
            int i = 0;
            while (i < int.MaxValue)
            {
                i++;
            }
        }

        [Benchmark]
        public void PrefixIncrement()
        {
            int i = 0;
            while (i < int.MaxValue)
            {
                ++i;
            }
        }
    }
}

/*
|           Method |     Mean |   Error |  StdDev | Ratio |
|----------------- |---------:|--------:|--------:|------:|
| PostfixIncrement | 658.0 ms | 1.40 ms | 1.31 ms |  1.00 |
|  PrefixIncrement | 655.1 ms | 1.07 ms | 0.90 ms |  1.00 |
*/