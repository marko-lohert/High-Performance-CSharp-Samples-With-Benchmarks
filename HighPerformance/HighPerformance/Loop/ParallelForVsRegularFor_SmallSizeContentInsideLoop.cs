namespace HighPerformance.Loop;

/// <summary>
/// Compare the difference in execution time between Parallel.For and regular for loop.
/// In this benchmark there is small content that in executed inside every step of a loop.
///
/// WARNING:
/// Because of a small size of content that in executed inside a loop,
/// and because of overhead of Parallel.For(that’s not generally speaking big),
/// in this case method containing Parallel.For is slower than method containing a regular for.
/// </summary>
public class ParallelForVsRegularFor_SmallSizeContentInsideLoop
{
    [Params(100, 1000, 10000, 200000)]
    public int Count { get; set; }

    public List<string> AllLines { get; set; }
    public int MaxAllowedLength { get; set; }

    [GlobalSetup]
    public void PrepareData()
    {
        AllLines = new();

        MaxAllowedLength = 100;

        for (int i = 0; i < Count; i++)
        {
            // For this benchmark we will use the same text in all test lines.
            AllLines.Add("This is a #test line that contains multiple #test hashtags.");
        }
    }

    [Benchmark(Baseline = true)]
    public void RegularFor()
    {
        // Count all lines that are longer than max allowed length.
        int counter = 0;

        for (int i = 0; i < Count; i++)
        {
            if (AllLines[i].Length > MaxAllowedLength)
                counter++;
        }
    }

    [Benchmark]
    public void ParallelFor()
    {
        // Count all lines that are longer than max allowed length.
        int counter = 0;

        Parallel.For(0, Count, (i) =>
        {
            if (AllLines[i].Length > MaxAllowedLength)
                counter++;
        });
    }
}


/*
WARNING:
Because of a small size of content that in executed inside a loop,
and because of overhead of Parallel.For (that’s not generally speaking big),
in this case method containing Parallel.For is slower than method containing a regular for.

|      Method |  Count |         Mean |       Error |       StdDev |       Median | Ratio | RatioSD |
|------------ |------- |-------------:|------------:|-------------:|-------------:|------:|--------:|
|  RegularFor |    100 |     105.9 ns |     2.15 ns |      3.02 ns |     105.5 ns |  1.00 |    0.00 |
| ParallelFor |    100 |   3,740.3 ns |    47.36 ns |     44.30 ns |   3,750.9 ns | 35.12 |    1.00 |
|             |        |              |             |              |              |       |         |
|  RegularFor |   1000 |     999.3 ns |    19.85 ns |     30.31 ns |     991.7 ns |  1.00 |    0.00 |
| ParallelFor |   1000 |   7,733.8 ns |   125.56 ns |    123.32 ns |   7,725.8 ns |  7.64 |    0.29 |
|             |        |              |             |              |              |       |         |
|  RegularFor |  10000 |   9,918.7 ns |   193.41 ns |    198.61 ns |   9,895.2 ns |  1.00 |    0.00 |
| ParallelFor |  10000 |  20,528.2 ns |   507.10 ns |  1,479.24 ns |  20,193.4 ns |  1.95 |    0.11 |
|             |        |              |             |              |              |       |         |
|  RegularFor | 200000 | 202,093.9 ns | 4,041.84 ns |  7,390.74 ns | 200,564.1 ns |  1.00 |    0.00 |
| ParallelFor | 200000 | 249,407.5 ns | 5,411.54 ns | 15,956.06 ns | 244,054.0 ns |  1.22 |    0.09 |
*/