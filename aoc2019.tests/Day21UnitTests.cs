using aoc2019.Code;
using Xunit;

public class Day21UnitTests
{
    [Theory]
    [InlineData(19355862)]
    public void Day21_Part1(long ans)
    {
        var d = new Day21();

        Assert.Equal(ans.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(1140470745)]
    public void Day21_Part2(long ans)
    {
        var d = new Day21();

        Assert.Equal(ans.ToString(), d.Part2());
    }
}