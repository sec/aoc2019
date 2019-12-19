using aoc2019.Code;
using Xunit;

public class Day16UnitTests
{
    [Theory]
    [InlineData(73127523)]
    public void Day16_Part1(long part1)
    {
        var d = new Day16();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(80284420)]
    public void Day16_Part2(long part2)
    {
        var d = new Day16();

        Assert.Equal(part2.ToString(), d.Part2());
    }

}