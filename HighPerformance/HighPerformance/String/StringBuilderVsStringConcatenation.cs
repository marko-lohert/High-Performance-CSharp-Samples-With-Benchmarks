using System.Text;

namespace HighPerformance.String;

/// <summary>
/// Compare the difference in both execution time and total memory used
/// in case of using StringBuilder and in case of concatenating strings.
/// </summary>
[MemoryDiagnoser]
public class StringBuilderVsStringConcatenation
{
    [Params(2, 3, 5, 7, 10, 15, 100, 1000)]
    public int ElementsCount { get; set; }

    [Params("a", "abcde")]
    public string SingleElement { get; set; }

    [Benchmark(Baseline = true)]
    public void StringConcatenation()
    {
        string result = string.Empty;
        for (int i = 0; i < ElementsCount; i++)
        {
            result += SingleElement;
        }
    }

    [Benchmark]
    public void StringBuilder()
    {
        StringBuilder result = new();
        for (int i = 0; i < ElementsCount; i++)
        {
            result.Append(SingleElement);
        }
    }
}


/*
|              Method | ElementsCount | SingleElement |          Mean |        Error |       StdDev |        Median | Ratio | RatioSD |     Gen 0 |   Allocated |
|-------------------- |-------------- |-------------- |--------------:|-------------:|-------------:|--------------:|------:|--------:|----------:|------------:|
| StringConcatenation |             2 |             a |      16.97 ns |     0.151 ns |     0.134 ns |      17.01 ns |  1.00 |    0.00 |    0.0102 |        32 B |
|       StringBuilder |             2 |             a |      18.95 ns |     0.180 ns |     0.160 ns |      19.00 ns |  1.12 |    0.01 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             2 |         abcde |      16.81 ns |     0.175 ns |     0.163 ns |      16.89 ns |  1.00 |    0.00 |    0.0153 |        48 B |
|       StringBuilder |             2 |         abcde |      21.98 ns |     0.323 ns |     0.302 ns |      22.01 ns |  1.31 |    0.02 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             3 |             a |      29.80 ns |     0.219 ns |     0.205 ns |      29.87 ns |  1.00 |    0.00 |    0.0204 |        64 B |
|       StringBuilder |             3 |             a |      22.05 ns |     0.185 ns |     0.164 ns |      22.07 ns |  0.74 |    0.01 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             3 |         abcde |      30.34 ns |     0.238 ns |     0.342 ns |      30.53 ns |  1.00 |    0.00 |    0.0331 |       104 B |
|       StringBuilder |             3 |         abcde |      28.08 ns |     0.279 ns |     0.261 ns |      28.20 ns |  0.92 |    0.01 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             5 |             a |      56.21 ns |     0.352 ns |     0.312 ns |      56.31 ns |  1.00 |    0.00 |    0.0408 |       128 B |
|       StringBuilder |             5 |             a |      29.66 ns |     0.614 ns |     1.008 ns |      29.25 ns |  0.53 |    0.02 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             5 |         abcde |      59.45 ns |     0.586 ns |     0.519 ns |      59.63 ns |  1.00 |    0.00 |    0.0764 |       240 B |
|       StringBuilder |             5 |         abcde |      66.93 ns |     0.638 ns |     0.596 ns |      67.33 ns |  1.13 |    0.02 |    0.0663 |       208 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             7 |             a |      82.44 ns |     0.812 ns |     0.759 ns |      83.00 ns |  1.00 |    0.00 |    0.0663 |       208 B |
|       StringBuilder |             7 |             a |      35.89 ns |     0.643 ns |     0.502 ns |      35.96 ns |  0.44 |    0.01 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |             7 |         abcde |      92.57 ns |     0.723 ns |     0.641 ns |      92.78 ns |  1.00 |    0.00 |    0.1351 |       424 B |
|       StringBuilder |             7 |         abcde |     110.90 ns |     2.240 ns |     2.579 ns |     110.84 ns |  1.19 |    0.03 |    0.1097 |       344 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |            10 |             a |     129.52 ns |     2.559 ns |     3.750 ns |     127.82 ns |  1.00 |    0.00 |    0.1070 |       336 B |
|       StringBuilder |            10 |             a |      46.82 ns |     0.179 ns |     0.150 ns |      46.85 ns |  0.36 |    0.01 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |            10 |         abcde |     150.36 ns |     0.411 ns |     0.364 ns |     150.30 ns |  1.00 |    0.00 |    0.2446 |       768 B |
|       StringBuilder |            10 |         abcde |     125.51 ns |     2.285 ns |     3.203 ns |     124.58 ns |  0.84 |    0.03 |    0.1097 |       344 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |            15 |             a |     193.23 ns |     2.093 ns |     1.957 ns |     193.65 ns |  1.00 |    0.00 |    0.1886 |       592 B |
|       StringBuilder |            15 |             a |      64.72 ns |     0.587 ns |     0.520 ns |      64.70 ns |  0.34 |    0.00 |    0.0331 |       104 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |            15 |         abcde |     266.28 ns |     4.814 ns |     7.774 ns |     263.51 ns |  1.00 |    0.00 |    0.4921 |     1,544 B |
|       StringBuilder |            15 |         abcde |     188.44 ns |     2.584 ns |     2.290 ns |     187.77 ns |  0.71 |    0.03 |    0.1733 |       544 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |           100 |             a |   2,056.62 ns |     6.053 ns |     5.055 ns |   2,056.06 ns |  1.00 |    0.00 |    4.0092 |    12,576 B |
|       StringBuilder |           100 |             a |     532.30 ns |    10.629 ns |    16.549 ns |     525.00 ns |  0.26 |    0.01 |    0.1726 |       544 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |           100 |         abcde |   5,276.32 ns |   103.074 ns |   134.025 ns |   5,285.66 ns |  1.00 |    0.00 |   16.8839 |    52,968 B |
|       StringBuilder |           100 |         abcde |     818.63 ns |    16.163 ns |    15.119 ns |     810.32 ns |  0.16 |    0.00 |    0.4635 |     1,456 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |          1000 |             a |  98,102.79 ns | 1,954.066 ns | 1,631.734 ns |  97,606.01 ns |  1.00 |    0.00 |  327.0264 | 1,025,976 B |
|       StringBuilder |          1000 |             a |   3,923.96 ns |    33.290 ns |    29.510 ns |   3,924.44 ns |  0.04 |    0.00 |    0.8087 |     2,552 B |
|                     |               |               |               |              |              |               |       |         |           |             |
| StringConcatenation |          1000 |         abcde | 415,962.33 ns | 8,223.494 ns | 7,692.262 ns | 417,034.47 ns |  1.00 |    0.00 | 1603.0273 | 5,029,968 B |
|       StringBuilder |          1000 |         abcde |   7,197.88 ns |   124.034 ns |   132.715 ns |   7,142.02 ns |  0.02 |    0.00 |    5.4474 |    17,104 B |*/