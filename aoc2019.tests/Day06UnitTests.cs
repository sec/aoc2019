using aoc2019.Code;
using Xunit;

public class Day06UnitTests
{
    [Theory]
    [InlineData(204521)]
    public void Day06_Part1(long part1)
    {
        var d = new Day06();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(307)]
    public void Day06_Part2(long part2)
    {
        var d = new Day06();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}