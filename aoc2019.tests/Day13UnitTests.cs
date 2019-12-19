using aoc2019.Code;
using Xunit;

public class Day13UnitTests
{
    [Theory]
    [InlineData(304)]
    public void Day13_Part1(long part1)
    {
        var d = new Day13();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(14747)]
    public void Day13_Part2(long part2)
    {
        var d = new Day13();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}