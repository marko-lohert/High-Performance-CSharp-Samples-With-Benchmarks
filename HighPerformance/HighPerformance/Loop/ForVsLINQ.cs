namespace HighPerformance.Loop
{
    /// <summary>
    /// Compare the execution time of for loop and LINQ.
    /// </summary>
    public class ForVsLINQ
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
        public void ForLoop()
        {
            int sum = 0;
            for (int i = 0; i < DataSize; i++)
            {
                sum += Data[i];
            }
        }

        [Benchmark]
        public void Linq()
        {
            int sum = Data.Sum();
        }
    }
}


/*
|  Method | DataSize |             Mean |           Error |          StdDev | Ratio | RatioSD |
|-------- |--------- |-----------------:|----------------:|----------------:|------:|--------:|
| ForLoop |       10 |         8.270 ns |       0.0659 ns |       0.0617 ns |  1.00 |    0.00 |
|    Linq |       10 |       108.115 ns |       0.7411 ns |       0.6569 ns | 13.06 |    0.14 |
|         |          |                  |                 |                 |       |         |
| ForLoop |      100 |        80.607 ns |       1.1483 ns |       1.0741 ns |  1.00 |    0.00 |
|    Linq |      100 |       724.919 ns |       4.6179 ns |       4.0937 ns |  8.99 |    0.12 |
|         |          |                  |                 |                 |       |         |
| ForLoop |     1000 |       735.232 ns |       6.1183 ns |       5.7231 ns |  1.00 |    0.00 |
|    Linq |     1000 |     6,991.395 ns |      51.3298 ns |      48.0139 ns |  9.51 |    0.11 |
|         |          |                  |                 |                 |       |         |
| ForLoop |    10000 |     7,390.614 ns |     132.7953 ns |     147.6016 ns |  1.00 |    0.00 |
|    Linq |    10000 |    69,105.330 ns |     485.8509 ns |     454.4653 ns |  9.32 |    0.20 |
|         |          |                  |                 |                 |       |         |
| ForLoop |  1000000 |   783,182.603 ns |   9,726.4690 ns |   8,622.2615 ns |  1.00 |    0.00 |
|    Linq |  1000000 | 6,922,424.548 ns | 136,910.1935 ns | 152,175.3173 ns |  8.92 |    0.16 |
*/