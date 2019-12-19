using aoc2019.Code;
using aoc2019.Misc;
using Xunit;

public class Day02UnitTests
{
    [Theory]
    [InlineData("1,0,0,0,99", 2)]
    [InlineData("1,1,1,4,99,5,6,0,99", 30)]
    public void Day02(string code, long ram)
    {
        var p = new IntCodeComputer(code.ToIntCode());
        p.Run();
        Assert.Equal(ram, p[0]);
    }

    [Theory]
    [InlineData(2842648)]
    public void Day02_Part01(long ram)
    {
        var d = new Day02();

        Assert.Equal(ram.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(9074)]
    public void Day02_Part02(long ram)
    {
        var d = new Day02();

        Assert.Equal(ram.ToString(), d.Part2());
    }
}