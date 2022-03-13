namespace HighPerformance.Array
{
    [MemoryDiagnoser]
    public class StackAllocVsRegularHeapAlloc_WithInit
    {
        [Params(5, 10, 100, 1000, 10000)]
        public int Size { get; set; }

        [Benchmark(Baseline = true)]
        public void RegularHeapAlloc()
        {
            Point3D[] array = new Point3D[Size];

            for (int i = 0; i < Size; i++)
            {
                array[i].X = 1;
                array[i].Y = 2;
                array[i].Z = 3;
            }
        }

        [Benchmark]
        public unsafe void StackAllocWithPointer()
        {
            Point3D* array = stackalloc Point3D[Size];

            for (int i = 0; i < Size; i++)
            {
                array[i].X = 1;
                array[i].Y = 2;
                array[i].Z = 3;
            }
        }

        [Benchmark]
        public void StackAllocWithSpan()
        {
            Span<Point3D> array = stackalloc Point3D[Size];

            for (int i = 0; i < Size; i++)
            {
                array[i].X = 1;
                array[i].Y = 2;
                array[i].Z = 3;
            }
        }

        public struct Point3D
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }
    }
}

/*
|                Method |  Size |          Mean |         Error |        StdDev | Ratio | RatioSD |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|---------------------- |------ |--------------:|--------------:|--------------:|------:|--------:|--------:|--------:|--------:|----------:|
|      RegularHeapAlloc |     5 |     12.919 ns |     0.2611 ns |     0.2443 ns |  1.00 |    0.00 |  0.0280 |       - |       - |      88 B |
| StackAllocWithPointer |     5 |      6.280 ns |     0.1236 ns |     0.1156 ns |  0.49 |    0.01 |       - |       - |       - |         - |
|    StackAllocWithSpan |     5 |      7.690 ns |     0.1653 ns |     0.2669 ns |  0.60 |    0.03 |       - |       - |       - |         - |
|                       |       |               |               |               |       |         |         |         |         |           |
|      RegularHeapAlloc |    10 |     20.643 ns |     0.2672 ns |     0.2368 ns |  1.00 |    0.00 |  0.0459 |       - |       - |     144 B |
| StackAllocWithPointer |    10 |     12.480 ns |     0.1213 ns |     0.1191 ns |  0.60 |    0.01 |       - |       - |       - |         - |
|    StackAllocWithSpan |    10 |     13.116 ns |     0.2659 ns |     0.2487 ns |  0.63 |    0.02 |       - |       - |       - |         - |
|                       |       |               |               |               |       |         |         |         |         |           |
|      RegularHeapAlloc |   100 |    161.137 ns |     1.4004 ns |     1.3099 ns |  1.00 |    0.00 |  0.3901 |       - |       - |   1,224 B |
| StackAllocWithPointer |   100 |    137.687 ns |     2.7673 ns |     4.2260 ns |  0.86 |    0.03 |       - |       - |       - |         - |
|    StackAllocWithSpan |   100 |    139.645 ns |     2.8240 ns |     2.6415 ns |  0.87 |    0.02 |       - |       - |       - |         - |
|                       |       |               |               |               |       |         |         |         |         |           |
|      RegularHeapAlloc |  1000 |  1,498.168 ns |    29.8208 ns |    81.6340 ns |  1.00 |    0.00 |  3.8166 |       - |       - |  12,024 B |
| StackAllocWithPointer |  1000 |  1,351.208 ns |    26.6978 ns |    37.4266 ns |  0.90 |    0.05 |       - |       - |       - |         - |
|    StackAllocWithSpan |  1000 |  1,350.720 ns |    14.3995 ns |    13.4693 ns |  0.89 |    0.06 |       - |       - |       - |         - |
|                       |       |               |               |               |       |         |         |         |         |           |
|      RegularHeapAlloc | 10000 | 64,456.535 ns | 1,441.1238 ns | 4,180.9614 ns |  1.00 |    0.00 | 36.9873 | 36.9873 | 36.9873 | 120,036 B |
| StackAllocWithPointer | 10000 | 14,150.741 ns |   281.1156 ns |   375.2813 ns |  0.23 |    0.01 |       - |       - |       - |         - |
|    StackAllocWithSpan | 10000 | 14,298.061 ns |   283.9495 ns |   407.2319 ns |  0.23 |    0.02 |       - |       - |       - |         - |
*/