using aoc2019.Code;
using Xunit;

public class Day24UnitTests
{
    [Theory]
    [InlineData(14539258)]
    public void Day24_Part1(long ans)
    {
        var d = new Day24();

        Assert.Equal(ans.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(1977)]
    public void Day24_Part2(long ans)
    {
        var d = new Day24();

        Assert.Equal(ans.ToString(), d.Part2());
    }
}