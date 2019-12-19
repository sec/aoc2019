using aoc2019.Code;
using Xunit;

public class Day12UnitTests
{
    [Theory]
    [InlineData(8454)]
    public void Day12_Part1(long part1)
    {
        var d = new Day12();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(362336016722948)]
    public void Day12_Part2(long part2)
    {
        var d = new Day12();

        Assert.Equal(part2.ToString(), d.Part2());
    }

}