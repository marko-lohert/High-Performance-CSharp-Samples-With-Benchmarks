namespace HighPerformance.Miscellaneous
{
    public class ClassVsStructVsRecordVsRecordStruct
    {
        [Benchmark(Baseline = true)]
        public void ClassPoint3D()
        {
            ClassPoint3D test = new ClassPoint3D() { X = 1, Y = 2, Z = 3 };
        }

        [Benchmark]
        public void StructPoint3D()
        {
            StructPoint3D test = new StructPoint3D() { X = 1, Y = 2, Z = 3 };
        }

        [Benchmark]
        public void RecordPoint3D()
        {
            RecordPoint3D test = new RecordPoint3D(1, 2, 3);
        }

        [Benchmark]
        public void RecordStrutPoint3D()
        {
            RecordStrutPoint3D test = new RecordStrutPoint3D(1, 2, 3);
        }
    }

    public class ClassPoint3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public struct StructPoint3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public record RecordPoint3D(int X, int Y, int Z);

    public record struct RecordStrutPoint3D(int X, int Y, int Z);
}

/*
|             Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|------------------- |----------:|----------:|----------:|----------:|------:|--------:|
|       ClassPoint3D | 3.6021 ns | 0.1040 ns | 0.1848 ns | 3.5671 ns | 1.000 |    0.00 |
|      StructPoint3D | 0.0127 ns | 0.0198 ns | 0.0250 ns | 0.0000 ns | 0.004 |    0.01 |
|      RecordPoint3D | 3.5765 ns | 0.0810 ns | 0.0758 ns | 3.5794 ns | 0.998 |    0.06 |
| RecordStrutPoint3D | 0.0108 ns | 0.0155 ns | 0.0129 ns | 0.0021 ns | 0.003 |    0.00 |
*/