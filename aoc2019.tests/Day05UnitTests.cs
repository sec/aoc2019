using aoc2019.Code;
using Xunit;

public class Day05UnitTests
{
    [Theory]
    [InlineData(9654885)]
    public void Day05_Part1(long part1)
    {
        var d = new Day05();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(7079459)]
    public void Day05_Part2(long part2)
    {
        var d = new Day05();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}