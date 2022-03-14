using System.Runtime.CompilerServices;

namespace HighPerformance.Miscellaneous;

/// <summary>
/// SkipLocalsInit attribute stops the compiler from inserting .locals init directive,
/// and without that directive JIT will not add a code that initializes local variables to default values.
/// </summary>
public class SkipLocalsInit
{
    [Params(100, 500, 1000, 5000, 10000)]
    public int Size { get; set; }

    [Benchmark(Baseline = true)]
    public void WithoutSkipLocalsInit()
    {
        Span<int> data = stackalloc int[Size];
    }

    [Benchmark]
    [SkipLocalsInit]
    // Note: if want to use [SkipLocalsInit] we need to compile with /unsafe 
    public void AddSkipLocalsInit()
    {
        Span<int> data = stackalloc int[Size];
    }
}


/*
|                Method |  Size |         Mean |      Error |     StdDev | Ratio |
|---------------------- |------ |-------------:|-----------:|-----------:|------:|
| WithoutSkipLocalsInit |   100 |    16.680 ns |  0.3636 ns |  0.5871 ns |  1.00 |
|     AddSkipLocalsInit |   100 |     2.425 ns |  0.0823 ns |  0.1699 ns |  0.15 |
|                       |       |              |            |            |       |
| WithoutSkipLocalsInit |   500 |    79.573 ns |  1.5920 ns |  1.5635 ns |  1.00 |
|     AddSkipLocalsInit |   500 |     2.461 ns |  0.0763 ns |  0.0676 ns |  0.03 |
|                       |       |              |            |            |       |
| WithoutSkipLocalsInit |  1000 |   159.478 ns |  3.1900 ns |  7.2005 ns |  1.00 |
|     AddSkipLocalsInit |  1000 |     2.418 ns |  0.0834 ns |  0.1983 ns |  0.02 |
|                       |       |              |            |            |       |
| WithoutSkipLocalsInit |  5000 |   773.120 ns | 15.5089 ns | 33.7151 ns | 1.000 |
|     AddSkipLocalsInit |  5000 |     4.849 ns |  0.1294 ns |  0.1385 ns | 0.006 |
|                       |       |              |            |            |       |
| WithoutSkipLocalsInit | 10000 | 1,695.629 ns | 20.4759 ns | 18.1514 ns | 1.000 |
|     AddSkipLocalsInit | 10000 |     7.026 ns |  0.0845 ns |  0.0706 ns | 0.004 |
*/