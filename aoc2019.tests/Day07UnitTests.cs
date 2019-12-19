using aoc2019.Code;
using Xunit;

public class Day07UnitTests
{
    [Theory]
    [InlineData(24625)]
    public void Day07_Part1(long part1)
    {
        var d = new Day07();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(36497698)]
    public void Day07_Part2(long part2)
    {
        var d = new Day07();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}