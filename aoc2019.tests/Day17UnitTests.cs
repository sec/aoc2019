using aoc2019.Code;
using Xunit;

public class Day17UnitTests
{
    [Theory]
    [InlineData(7328)]
    public void Day17_Part1(long part1)
    {
        var d = new Day17();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(1289413)]
    public void Day17_Part2(long part2)
    {
        var d = new Day17();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}