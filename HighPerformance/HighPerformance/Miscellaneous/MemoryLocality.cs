namespace HighPerformance.Miscellaneous
{
    /// <summary>
    /// Test how memory locality influences the execution time
    /// -> an order in which we access memory in 2D array makes a difference.
    /// </summary>
    public class MemoryLocality
    {
        [Params(100, 500, 1000, 5000, 10000)]
        public int Size { get; set; }
        private int[,] data;

        [GlobalSetup]
        public void PrepareData()
        {
            data = new int[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    data[i, j] = Random.Shared.Next();
                }
            }
        }

        [Benchmark(Baseline = true)]
        public void CountZerosOuterInner()
        {
            int counter = 0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (data[i, j] == 0)
                        counter++;
                }
            }
        }


        [Benchmark]
        public void CountZerosInnerOuterInner()
        {
            int counter = 0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (data[j, i] == 0)
                        counter++;
                }
            }
        }

    }
}


/*
|                    Method |  Size |            Mean |         Error |        StdDev |          Median | Ratio | RatioSD |
|-------------------------- |------ |----------------:|--------------:|--------------:|----------------:|------:|--------:|
|      CountZerosOuterInner |   100 |        12.60 us |      0.250 us |      0.382 us |        12.48 us |  1.00 |    0.00 |
| CountZerosInnerOuterInner |   100 |        13.19 us |      0.260 us |      0.549 us |        12.99 us |  1.05 |    0.06 |
|                           |       |                 |               |               |                 |       |         |
|      CountZerosOuterInner |   500 |       305.90 us |      5.138 us |      4.555 us |       307.42 us |  1.00 |    0.00 |
| CountZerosInnerOuterInner |   500 |       364.19 us |      6.803 us |     13.268 us |       358.38 us |  1.22 |    0.05 |
|                           |       |                 |               |               |                 |       |         |
|      CountZerosOuterInner |  1000 |     1,269.33 us |     14.550 us |     12.898 us |     1,263.25 us |  1.00 |    0.00 |
| CountZerosInnerOuterInner |  1000 |     1,904.86 us |     43.874 us |    125.883 us |     1,926.07 us |  1.60 |    0.07 |
|                           |       |                 |               |               |                 |       |         |
|      CountZerosOuterInner |  5000 |    31,735.39 us |    289.251 us |    241.537 us |    31,831.28 us |  1.00 |    0.00 |
| CountZerosInnerOuterInner |  5000 |   361,336.49 us |  4,224.527 us |  3,951.625 us |   360,519.40 us | 11.36 |    0.12 |
|                           |       |                 |               |               |                 |       |         |
|      CountZerosOuterInner | 10000 |   140,176.07 us |  2,973.082 us |  8,766.200 us |   135,380.38 us |  1.00 |    0.00 |
| CountZerosInnerOuterInner | 10000 | 1,634,403.54 us | 31,595.545 us | 26,383.711 us | 1,629,835.90 us | 11.65 |    0.96 |
*/