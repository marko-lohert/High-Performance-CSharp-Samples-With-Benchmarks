using System.Buffers;

namespace HighPerformance.Array
{
    /// <summary>
    /// Compare how long it takes to allocate an array using ArrayPool
    /// compared to allocating an array using operator new.
    /// </summary>
    public class ArrayPoolVsRegularArrayAllocation
    {
        [Params(100, 500, 1000, 5000, 10000)]
        public int Size { get; set; }

        [Benchmark(Baseline = true)]
        public void AllocateArrayWithNew()
        {
            int[] array = new int[Size];
        }

        [Benchmark]
        public void ArrayPool()
        {
            int[] array = ArrayPool<int>.Shared.Rent(Size);
        }
    }
}


/*
|               Method |  Size |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |------ |------------:|----------:|----------:|------:|--------:|
| AllocateArrayWithNew |   100 |    25.20 ns |  0.878 ns |  2.561 ns |  1.00 |    0.00 |
|            ArrayPool |   100 |    43.33 ns |  1.012 ns |  2.952 ns |  1.74 |    0.23 |
|                      |       |             |           |           |       |         |
| AllocateArrayWithNew |   500 |   110.07 ns |  2.197 ns |  6.443 ns |  1.00 |    0.00 |
|            ArrayPool |   500 |   119.57 ns |  2.422 ns |  2.693 ns |  1.07 |    0.08 |
|                      |       |             |           |           |       |         |
| AllocateArrayWithNew |  1000 |   202.09 ns |  3.792 ns |  3.362 ns |  1.00 |    0.00 |
|            ArrayPool |  1000 |   127.16 ns |  1.975 ns |  1.649 ns |  0.63 |    0.01 |
|                      |       |             |           |           |       |         |
| AllocateArrayWithNew |  5000 |   845.20 ns | 16.890 ns | 20.743 ns |  1.00 |    0.00 |
|            ArrayPool |  5000 |   283.74 ns |  1.777 ns |  1.575 ns |  0.33 |    0.01 |
|                      |       |             |           |           |       |         |
| AllocateArrayWithNew | 10000 | 1,770.75 ns | 35.283 ns | 78.916 ns |  1.00 |    0.00 |
|            ArrayPool | 10000 |   465.02 ns |  5.785 ns |  4.831 ns |  0.26 |    0.01 |
*/