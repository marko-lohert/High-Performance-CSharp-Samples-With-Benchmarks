using System.Numerics;

namespace HighPerformance.SIMD;

/// <summary>
/// Use SIMD instructions to speed up numeric operations (addition in this sample)
/// executed on a large amount of data.
/// </summary>
public class SIMDSumVsForSum
{
    [Params(8, 16, 800, 80000, 800000)]
    public int DataCount { get; set; }
    public double[] Data;

    double maxDataValue = 100.0;

    [GlobalSetup]
    public void PrepData()
    {
        Data = new double[DataCount];
        for (int i = 0; i < DataCount; i++)
        {
            Data[i] = Random.Shared.NextDouble() * maxDataValue;
        }
    }

    [Benchmark(Baseline = true)]
    public double ForSum()
    {
        double totalSum = 0;
        for (int i = 0; i < DataCount; i++)
        {
            totalSum += Data[i];
        }

        return totalSum;
    }
    
    [Benchmark]
    public double SIMDSum()
    {
        if (!Vector.IsHardwareAccelerated)
            return 0;

        int vectorSize = Vector<double>.Count;
        Vector<double> vectorIntermediateSums = Vector<double>.Zero;

        // Add multiple data at the same time using SIMD instructions.
        // Result is in the vector that contains intermediate sums.
        for (int i = 0; i <= DataCount - vectorSize; i += vectorSize)
        {
            Vector<double> vectorNextDataGroup = new Vector<double>(Data, i);
            vectorIntermediateSums = Vector.Add(vectorIntermediateSums, vectorNextDataGroup);
        }

        // Total sum = sum of intermediate sums contained in a vectorSum.
        double totalSum = 0;
        for (int i = 0; i < vectorSize; i++)
        {
            totalSum += vectorIntermediateSums[i];
        }

        return totalSum;
    }
}


/*

In case we use Vector<double> :


|  Method | DataCount |           Mean |          Error |         StdDev | Ratio | RatioSD |
|-------- |---------- |---------------:|---------------:|---------------:|------:|--------:|
|  ForSum |         8 |       5.391 ns |      0.0690 ns |      0.0612 ns |  1.00 |    0.00 |
| SIMDSum |         8 |       4.674 ns |      0.1287 ns |      0.2078 ns |  0.86 |    0.04 |
|         |           |                |                |                |       |         |
|  ForSum |        16 |      10.643 ns |      0.0498 ns |      0.0466 ns |  1.00 |    0.00 |
| SIMDSum |        16 |       5.964 ns |      0.0361 ns |      0.0320 ns |  0.56 |    0.00 |
|         |           |                |                |                |       |         |
|  ForSum |       800 |     716.712 ns |     13.9916 ns |     13.7416 ns |  1.00 |    0.00 |
| SIMDSum |       800 |     192.884 ns |      0.6396 ns |      0.5341 ns |  0.27 |    0.01 |
|         |           |                |                |                |       |         |
|  ForSum |     80000 |  71,324.095 ns |    470.6004 ns |    440.1999 ns |  1.00 |    0.00 |
| SIMDSum |     80000 |  21,994.420 ns |    435.4059 ns |    638.2123 ns |  0.31 |    0.01 |
|         |           |                |                |                |       |         |
|  ForSum |    800000 | 819,883.346 ns | 10,477.4626 ns |  8,749.1558 ns |  1.00 |    0.00 |
| SIMDSum |    800000 | 355,615.560 ns |  6,483.6445 ns | 14,094.9095 ns |  0.43 |    0.02 |


In case we use Vector<int> :

|  Method | DataCount |           Mean |         Error |        StdDev | Ratio | RatioSD |
|-------- |---------- |---------------:|--------------:|--------------:|------:|--------:|
|  ForSum |         8 |       4.623 ns |     0.0645 ns |     0.0539 ns |  1.00 |    0.00 |
| SIMDSum |         8 |       6.750 ns |     0.0882 ns |     0.0825 ns |  1.46 |    0.02 |
|         |           |                |               |               |       |         |
|  ForSum |        16 |       9.383 ns |     0.2106 ns |     0.2586 ns |  1.00 |    0.00 |
| SIMDSum |        16 |       6.953 ns |     0.0965 ns |     0.0855 ns |  0.74 |    0.03 |
|         |           |                |               |               |       |         |
|  ForSum |       800 |     504.874 ns |     5.4254 ns |     5.0749 ns |  1.00 |    0.00 |
| SIMDSum |       800 |      83.568 ns |     0.8576 ns |     0.7602 ns |  0.17 |    0.00 |
|         |           |                |               |               |       |         |
|  ForSum |     80000 |  50,506.353 ns |   711.0710 ns |   665.1363 ns |  1.00 |    0.00 |
| SIMDSum |     80000 |   8,784.096 ns |    94.2397 ns |    88.1519 ns |  0.17 |    0.00 |
|         |           |                |               |               |       |         |
|  ForSum |    800000 | 499,818.522 ns | 4,792.6594 ns | 4,483.0566 ns |  1.00 |    0.00 |
| SIMDSum |    800000 |  89,732.721 ns | 1,116.2237 ns | 1,044.1163 ns |  0.18 |    0.00 |
*/