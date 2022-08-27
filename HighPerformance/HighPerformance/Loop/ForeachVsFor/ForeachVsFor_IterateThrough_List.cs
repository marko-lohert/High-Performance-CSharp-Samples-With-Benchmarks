namespace HighPerformance.Loop;

/// <summary>
/// Compare the execution time of foreach loop and for loop
/// by iterating through a List<int>.
/// 
/// NOTE:
/// Ratio of execution times is not the same in a case foreach and for loops are iterating through
///   a) array int[]
///   b) List<int>
/// Please, compare result of this benchmark, where data is stored in a List<int>,
/// with the result of benchmark <see cref="ForeachVsFor_IterateThrough_Array" /> where data is stored in an array int[].
/// </summary>
public class ForeachVsFor_IterateThrough_List
{
    [Params(10, 100, 1000, 10000, 1000000)]
    public int DataSize { get; set; }
    List<int> Data;

    [GlobalSetup]
    public void PrepareData()
    {
        Data = new List<int>(DataSize);

        for (int i = 0; i < DataSize; i++)
        {
            Data.Add(Random.Shared.Next(minValue: 0, maxValue: 10));
        }
    }

    [Benchmark(Baseline = true)]
    public int ForEach()
    {
        int sum = 0;
        foreach (int dataElement in Data)
        {
            sum += dataElement;
        }

        return sum;
    }

    [Benchmark]
    public int For()
    {
        int sum = 0;
        for (int i = 0; i < Data.Count; i++)
        {
            sum += Data[i];
        }

        return sum;
    }
}


/*
NOTE:
Ratio of execution times is not the same in a case foreach and for loops are iterating through
  a) array int[]
  b) List<int>
Please, compare result of this benchmark, where data is stored in a List<int>,
with the result of benchmark "ForeachVsFor_IterateThrough_Array" where data is stored in an array int[].

|  Method | DataSize |             Mean |         Error |        StdDev |           Median | Ratio | RatioSD |
|-------- |--------- |-----------------:|--------------:|--------------:|-----------------:|------:|--------:|
| ForEach |       10 |        13.823 ns |     0.1696 ns |     0.1503 ns |        13.790 ns |  1.00 |    0.00 |
|     For |       10 |         6.843 ns |     0.0278 ns |     0.0246 ns |         6.842 ns |  0.50 |    0.01 |
|         |          |                  |               |               |                  |       |         |
| ForEach |      100 |       139.407 ns |     4.1846 ns |    12.3384 ns |       130.718 ns |  1.00 |    0.00 |
|     For |      100 |        72.098 ns |     0.4514 ns |     0.3769 ns |        72.069 ns |  0.51 |    0.05 |
|         |          |                  |               |               |                  |       |         |
| ForEach |     1000 |     1,252.921 ns |    18.2141 ns |    16.1464 ns |     1,244.649 ns |  1.00 |    0.00 |
|     For |     1000 |       661.380 ns |    12.6901 ns |    19.3792 ns |       656.249 ns |  0.54 |    0.02 |
|         |          |                  |               |               |                  |       |         |
| ForEach |    10000 |    12,478.110 ns |   242.1307 ns |   226.4892 ns |    12,386.671 ns |  1.00 |    0.00 |
|     For |    10000 |     6,422.096 ns |   115.2107 ns |    89.9490 ns |     6,410.484 ns |  0.51 |    0.01 |
|         |          |                  |               |               |                  |       |         |
| ForEach |  1000000 | 1,234,919.613 ns | 6,070.0793 ns | 4,739.1186 ns | 1,233,545.020 ns |  1.00 |    0.00 |
|     For |  1000000 |   630,146.973 ns | 2,276.8663 ns | 2,129.7822 ns |   629,270.020 ns |  0.51 |    0.00 |
*/