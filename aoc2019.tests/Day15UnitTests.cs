using aoc2019.Code;
using Xunit;

public class Day15UnitTests
{
    [Theory]
    [InlineData(222)]
    public void Day15_Part1(long part1)
    {
        var d = new Day15();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(394)]
    public void Day15_Part2(long part2)
    {
        var d = new Day15();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}