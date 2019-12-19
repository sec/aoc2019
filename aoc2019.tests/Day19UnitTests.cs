using aoc2019.Code;
using Xunit;

public class Day19UnitTests
{
    [Theory]
    [InlineData(118)]
    public void Day19_Part1(long ans)
    {
        var d = new Day19();

        Assert.Equal(ans.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(18651593)]
    public void Day19_Part2(long ans)
    {
        var d = new Day19();

        Assert.Equal(ans.ToString(), d.Part2());
    }
}