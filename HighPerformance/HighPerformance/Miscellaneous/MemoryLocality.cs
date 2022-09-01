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
        public void AccessArray_RowByRow()
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
        public void AccessArray_JumpBetweenRows()
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
|                      Method |  Size |            Mean |         Error |        StdDev | Ratio | RatioSD |
|---------------------------- |------ |----------------:|--------------:|--------------:|------:|--------:|
|        AccessArray_RowByRow |   100 |        12.59 us |      0.044 us |      0.034 us |  1.00 |    0.00 |
| AccessArray_JumpBetweenRows |   100 |        12.87 us |      0.245 us |      0.301 us |  1.03 |    0.03 |
|                             |       |                 |               |               |       |         |
|        AccessArray_RowByRow |   500 |       304.24 us |      4.398 us |      4.114 us |  1.00 |    0.00 |
| AccessArray_JumpBetweenRows |   500 |       328.45 us |      4.721 us |      4.185 us |  1.08 |    0.02 |
|                             |       |                 |               |               |       |         |
|        AccessArray_RowByRow |  1000 |     1,237.09 us |     24.163 us |     36.900 us |  1.00 |    0.00 |
| AccessArray_JumpBetweenRows |  1000 |     1,738.65 us |     32.645 us |     36.284 us |  1.39 |    0.05 |
|                             |       |                 |               |               |       |         |
|        AccessArray_RowByRow |  5000 |    32,816.17 us |    128.020 us |    119.750 us |  1.00 |    0.00 |
| AccessArray_JumpBetweenRows |  5000 |   386,032.43 us |  7,462.009 us |  9,437.052 us | 11.90 |    0.25 |
|                             |       |                 |               |               |       |         |
|        AccessArray_RowByRow | 10000 |   132,156.57 us |    325.311 us |    304.296 us |  1.00 |    0.00 |
| AccessArray_JumpBetweenRows | 10000 | 1,701,207.87 us | 32,847.171 us | 48,146.957 us | 12.86 |    0.37 |
*/