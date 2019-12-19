using aoc2019.Code;
using Xunit;

public class Day10UnitTests
{
    [Theory]
    [InlineData(214)]
    public void Day10_Part1(long part1)
    {
        var d = new Day10();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(502)]
    public void Day10_Part2(long part2)
    {
        var d = new Day10();
        d.Part1();

        Assert.Equal(part2.ToString(), d.Part2());
    }
}