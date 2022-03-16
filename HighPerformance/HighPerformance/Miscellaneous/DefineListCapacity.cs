namespace HighPerformance.Miscellaneous;

/// <summary>
/// Defining a capacity of a list when allocating that list will increase performance.
/// </summary>
public class DefineListCapacity
{
    [Params(10, 10000, 100000)]
    public int Capacity { get; set; }

    [Benchmark(Baseline = true)]
    public void AllocWithoutDefiningCapacity_FillList()
    {
        List<int> testList = new List<int>();

        for (int i = 0; i < Capacity; i++)
        {
            testList.Add(i);
        }
    }

    [Benchmark]
    public void AllocDefineCapacity_FillList()
    {
        List<int> testList = new List<int>(Capacity);

        for (int i = 0; i < Capacity; i++)
        {
            testList.Add(i);
        }
    }
}


/*
|                                Method | Capacity |          Mean |         Error |        StdDev |        Median | Ratio | RatioSD |
|-------------------------------------- |--------- |--------------:|--------------:|--------------:|--------------:|------:|--------:|
| AllocWithoutDefiningCapacity_FillList |       10 |      71.45 ns |      1.471 ns |      4.124 ns |      70.84 ns |  1.00 |    0.00 |
|          AllocDefineCapacity_FillList |       10 |      31.82 ns |      0.792 ns |      2.298 ns |      31.59 ns |  0.44 |    0.04 |
|                                       |          |               |               |               |               |       |         |
| AllocWithoutDefiningCapacity_FillList |    10000 |  30,164.67 ns |    597.011 ns |  1,703.306 ns |  29,519.48 ns |  1.00 |    0.00 |
|          AllocDefineCapacity_FillList |    10000 |  20,878.67 ns |    174.069 ns |    162.824 ns |  20,968.55 ns |  0.67 |    0.04 |
|                                       |          |               |               |               |               |       |         |
| AllocWithoutDefiningCapacity_FillList |   100000 | 561,780.08 ns | 21,082.435 ns | 62,162.036 ns | 538,123.68 ns |  1.00 |    0.00 |
|          AllocDefineCapacity_FillList |   100000 | 344,258.14 ns |  2,702.511 ns |  2,109.943 ns | 344,899.85 ns |  0.64 |    0.01 |
*/