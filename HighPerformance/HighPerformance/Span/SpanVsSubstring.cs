namespace HighPerformance.Span
{
    public class SpanVsSubstring
    {
        [Benchmark(Baseline = true)]
        [Arguments("abcdefghijklmno")]
        public string Substring(string str)
        {
            string midStr = str.Substring(7, 3);

            return midStr;
        }

        [Benchmark]
        [Arguments("abcdefghijklmno")]
        public ReadOnlySpan<char> Span(string str)
        {
            ReadOnlySpan<char> midStr = str.AsSpan().Slice(7, 3);

            return midStr;
        }
    }
}

/*
|    Method |             str |      Mean |     Error |    StdDev | Ratio |
|---------- |---------------- |----------:|----------:|----------:|------:|
| Substring | abcdefghijklmno | 9.0950 ns | 0.2647 ns | 0.3962 ns |  1.00 |
|      Span | abcdefghijklmno | 0.4237 ns | 0.0106 ns | 0.0094 ns |  0.05 | 
*/