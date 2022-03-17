using System.Text.RegularExpressions;

namespace HighPerformance.Loop;

/// <summary>
/// Compare the difference in execution time between Parallel.For and regular for loop.
/// In this benchmark there is medium-sized content that in executed inside every step of a loop.
/// </summary>
public class ParallelForVsRegularFor
{
    [Params(100, 1000, 10000, 200000)]
    public int Count { get; set; }

    public List<string> AllLines { get;set; }

    [GlobalSetup]
    public void PrepareData()
    {
        AllLines = new();

        for (int i = 0; i < Count; i++)
        {
            // For this benchmark we will use the same text in all test lines.
            AllLines.Add("This is a #test line that contains multiple #test hashtags.");
        }
    }

    [Benchmark(Baseline = true)]
    public void RegularFor()
    {
        // Count of all lines that contain 2 or more same hashtags.
        int counter = 0;

        for (int i = 0; i < Count; i++)
        {
            if (DoesContainMultipleHashtags(AllLines[i]))
                counter++;
        }
    }

    [Benchmark]
    public void ParallelFor()
    {
        // Count of all lines that contain 2 or more same hashtags.
        int counter = 0;

        Parallel.For(0, Count, (i) =>
        {
            if (DoesContainMultipleHashtags(AllLines[i]))
                counter++;
        });
    }

    private bool DoesContainMultipleHashtags(string text)
    {
        Regex regexHashtag = new(@"(#[a-zA-Z0-9_-]+).*(\1)", RegexOptions.IgnoreCase);
        int count = regexHashtag.Matches(text).Count;

        return count > 0;
    }
}


/*
|      Method |  Count |           Mean |        Error |       StdDev |         Median | Ratio |
|------------ |------- |---------------:|-------------:|-------------:|---------------:|------:|
|  RegularFor |    100 |       531.6 us |      7.49 us |      7.01 us |       529.8 us |  1.00 |
| ParallelFor |    100 |       155.9 us |      2.37 us |      2.53 us |       155.0 us |  0.29 |
|             |        |                |              |              |                |       |
|  RegularFor |   1000 |     5,249.7 us |     33.67 us |     26.29 us |     5,249.4 us |  1.00 |
| ParallelFor |   1000 |     1,274.8 us |     21.90 us |     18.29 us |     1,270.8 us |  0.24 |
|             |        |                |              |              |                |       |
|  RegularFor |  10000 |    54,460.4 us |  1,069.45 us |  1,313.38 us |    55,097.8 us |  1.00 |
| ParallelFor |  10000 |    13,033.1 us |    258.97 us |    734.65 us |    12,747.3 us |  0.24 |
|             |        |                |              |              |                |       |
|  RegularFor | 200000 | 1,107,010.3 us | 20,674.37 us | 19,338.82 us | 1,116,619.8 us |  1.00 |
| ParallelFor | 200000 |   260,507.7 us |  3,351.28 us |  3,134.79 us |   261,325.6 us |  0.24 |
*/