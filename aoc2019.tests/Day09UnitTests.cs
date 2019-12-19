using aoc2019.Code;
using Xunit;

public class Day09UnitTests
{
    [Theory]
    [InlineData(2350741403)]
    public void Day09_Part1(long part1)
    {
        var d = new Day09();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(53088)]
    public void Day09_Part2(long part2)
    {
        var d = new Day09();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}