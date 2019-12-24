using aoc2019.Code;
using Xunit;

public class Day24UnitTests
{
    [Theory]
    [InlineData(14539258)]
    public void Day24_Part1(long part1)
    {
        var d = new Day24();

        Assert.Equal(part1.ToString(), d.Part1());
    }
}