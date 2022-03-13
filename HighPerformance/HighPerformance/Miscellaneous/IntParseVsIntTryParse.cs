namespace HighPerformance.Miscellaneous;

/// <summary>
/// Compare the execution time of parsing an int with int.Parse() and int.TryParse().
/// </summary>
public class IntParseVsIntTryParse
{
    [Params("1", "2147483647", "X")]
    public string Data { get; set; }

    [Benchmark(Baseline = true)]
    public void IntParse()
    {
        int result;
        try
        {
            result = int.Parse(Data);
        }
        catch
        {
            // Write a message in the log.
        }
    }

    [Benchmark]
    public void IntTryParse()
    {
        bool succedded = int.TryParse(Data, out int result);
    }
}


/*
|      Method |       Data |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD |
|------------ |----------- |------------:|-----------:|-----------:|------------:|------:|--------:|
|    IntParse |          1 |    12.48 ns |   0.056 ns |   0.046 ns |    12.48 ns |  1.00 |    0.00 |
| IntTryParse |          1 |    11.19 ns |   0.210 ns |   0.308 ns |    11.15 ns |  0.91 |    0.02 |
|             |            |             |            |            |             |       |         |
|    IntParse | 2147483647 |    23.73 ns |   0.501 ns |   0.669 ns |    23.40 ns |  1.00 |    0.00 |
| IntTryParse | 2147483647 |    23.22 ns |   0.475 ns |   0.566 ns |    23.00 ns |  0.98 |    0.04 |
|             |            |             |            |            |             |       |         |
|    IntParse |          X | 7,375.05 ns | 147.158 ns | 422.225 ns | 7,254.55 ns | 1.000 |    0.00 |
| IntTryParse |          X |    10.68 ns |   0.245 ns |   0.229 ns |    10.67 ns | 0.001 |    0.00 |
*/