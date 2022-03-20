namespace HighPerformance.Loop;

/// <summary>
/// Compare the execution time of foreach loop and for loop
/// when they iterate through an array of integers.
/// 
/// NOTE:
/// Ratio of execution times is not the same in a case foreach and for loops are iterating through
///   a) array int[]
///   b) List<int>
/// Please, compare result of this benchmark, where data is stored in an array int[],
/// with the result of benchmark <see cref="ForeachVsFor_IterateThrough_List" /> where data is stored in a List<int>. 
/// </summary>
public class ForeachVsFor_IterateThrough_Array
{
    [Params(10, 100, 1000, 10000, 1000000)]
    public int DataSize { get; set; }
    int[] Data;

    [GlobalSetup]
    public void PrepareData()
    {
        Data = new int[DataSize];

        for (int i = 0; i < DataSize; i++)
        {
            Data[i] = Random.Shared.Next(minValue: 0, maxValue: 10);
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
        for (int i = 0; i < Data.Length; i++)
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
Please, compare result of this benchmark, where data is stored in an array int[],
with the result of benchmark "ForeachVsFor_IterateThrough_List" where data is stored in a List<int>. 

|  Method | DataSize |           Mean |         Error |        StdDev | Ratio | RatioSD |
|-------- |--------- |---------------:|--------------:|--------------:|------:|--------:|
| ForEach |       10 |       3.794 ns |     0.0211 ns |     0.0197 ns |  1.00 |    0.00 |
|     For |       10 |       7.015 ns |     0.0378 ns |     0.0353 ns |  1.85 |    0.01 |
|         |          |                |               |               |       |         |
| ForEach |      100 |      56.294 ns |     0.4838 ns |     0.4525 ns |  1.00 |    0.00 |
|     For |      100 |      65.189 ns |     0.4562 ns |     0.4267 ns |  1.16 |    0.02 |
|         |          |                |               |               |       |         |
| ForEach |     1000 |     499.031 ns |     2.2878 ns |     2.1400 ns |  1.00 |    0.00 |
|     For |     1000 |     606.240 ns |     3.9743 ns |     3.7176 ns |  1.21 |    0.01 |
|         |          |                |               |               |       |         |
| ForEach |    10000 |   4,838.184 ns |    10.2458 ns |     8.5557 ns |  1.00 |    0.00 |
|     For |    10000 |   6,005.364 ns |     3.8849 ns |     3.2441 ns |  1.24 |    0.00 |
|         |          |                |               |               |       |         |
| ForEach |  1000000 | 417,287.484 ns | 1,646.2392 ns | 1,539.8932 ns |  1.00 |    0.00 |
|     For |  1000000 | 599,871.966 ns | 2,026.4008 ns | 1,895.4965 ns |  1.44 |    0.01 |
*/