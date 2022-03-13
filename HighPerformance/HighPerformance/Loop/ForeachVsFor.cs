namespace HighPerformance.Loop;

/// <summary>
/// Compare the execution time of foreach loop and for loop.
/// </summary>
public class ForeachVsFor
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
    public void ForEach()
    {
        int sum = 0;
        foreach (int i in Data)
        {
            sum += Data[i];
        }
    }

    [Benchmark]
    public void ForLoop()
    {
        int sum = 0;
        for (int i = 0; i < DataSize; i++)
        {
            sum += Data[i];
        }
    }
}


/*
|  Method | DataSize |             Mean |          Error |         StdDev | Ratio |
|-------- |--------- |-----------------:|---------------:|---------------:|------:|
| ForEach |       10 |        16.402 ns |      0.3537 ns |      0.3309 ns |  1.00 |
| ForLoop |       10 |         8.222 ns |      0.0846 ns |      0.0792 ns |  0.50 |
|         |          |                  |                |                |       |
| ForEach |      100 |       154.917 ns |      1.2213 ns |      1.1424 ns |  1.00 |
| ForLoop |      100 |        80.419 ns |      1.6204 ns |      1.9900 ns |  0.52 |
|         |          |                  |                |                |       |
| ForEach |     1000 |     1,491.013 ns |      5.5105 ns |      4.8850 ns |  1.00 |
| ForLoop |     1000 |       721.757 ns |      4.6487 ns |      4.3484 ns |  0.48 |
|         |          |                  |                |                |       |
| ForEach |    10000 |    14,818.410 ns |     70.0942 ns |     62.1367 ns |  1.00 |
| ForLoop |    10000 |     7,181.341 ns |     13.2981 ns |     11.1045 ns |  0.48 |
|         |          |                  |                |                |       |
| ForEach |  1000000 | 1,519,259.587 ns |  1,765.9760 ns |  1,378.7579 ns |  1.00 |
| ForLoop |  1000000 |   760,227.763 ns | 14,213.8359 ns | 19,925.8127 ns |  0.50 |
*/