namespace HighPerformance.Miscellaneous
{
    /// <summary>
    /// Compare the execution time of if and switch statements.
    /// </summary>
    public class IfVsSwitch
    {
        [Benchmark(Baseline = true)]
        [Arguments(1)]
        [Arguments(2)]
        [Arguments(3)]
        [Arguments(4)]
        [Arguments(5)]
        [Arguments(6)]
        public string If(int place)
        {
            if (place == 1)
                return "Gold medal";
            else if (place == 2)
                return "Silver medal";
            else if (place == 3)
                return "Bronze medal";
            else if (place == 4 || place == 5)
                return "Almost a medal";
            else
                return "Practice makes perfect";
        }

        [Benchmark]
        [Arguments(1)]
        [Arguments(2)]
        [Arguments(3)]
        [Arguments(4)]
        [Arguments(5)]
        [Arguments(6)]
        public string Switch(int place)
        {
            switch (place)
            {
                case 1:
                    return "Gold medal";
                case 2:
                    return "Silver medal";
                case 3:
                    return "Bronze medal";
                case 4:
                case 5:
                    return "Almost a medal";
                default:
                    return "Practice makes perfect";
            };
        }
    }
}

/*
| Method | place |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------- |------ |----------:|----------:|----------:|------:|--------:|
|     If |     1 | 0.3140 ns | 0.0224 ns | 0.0198 ns |  1.00 |    0.00 |
| Switch |     1 | 0.5915 ns | 0.0135 ns | 0.0126 ns |  1.89 |    0.13 |
|        |       |           |           |           |       |         |
|     If |     2 | 0.9255 ns | 0.0078 ns | 0.0073 ns |  1.00 |    0.00 |
| Switch |     2 | 0.6026 ns | 0.0163 ns | 0.0152 ns |  0.65 |    0.02 |
|        |       |           |           |           |       |         |
|     If |     3 | 0.9322 ns | 0.0135 ns | 0.0119 ns |  1.00 |    0.00 |
| Switch |     3 | 0.8815 ns | 0.0163 ns | 0.0145 ns |  0.95 |    0.02 |
|        |       |           |           |           |       |         |
|     If |     4 | 1.5349 ns | 0.0153 ns | 0.0143 ns |  1.00 |    0.00 |
| Switch |     4 | 0.6030 ns | 0.0131 ns | 0.0122 ns |  0.39 |    0.01 |
|        |       |           |           |           |       |         |
|     If |     5 | 1.2462 ns | 0.0184 ns | 0.0163 ns |  1.00 |    0.00 |
| Switch |     5 | 0.5809 ns | 0.0163 ns | 0.0152 ns |  0.47 |    0.01 |
|        |       |           |           |           |       |         |
|     If |     6 | 1.5223 ns | 0.0203 ns | 0.0190 ns |  1.00 |    0.00 |
| Switch |     6 | 0.2894 ns | 0.0123 ns | 0.0115 ns |  0.19 |    0.01 |
*/