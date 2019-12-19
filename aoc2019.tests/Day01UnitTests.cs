using aoc2019.Code;
using Xunit;

public class Day01UnitTests
{
    [Fact]
    public void Day01()
    {
        Day01 p = new Day01();

        Assert.Equal(3422661.ToString(), p.Part1());
        Assert.Equal(5131103.ToString(), p.Part2());
    }

    [Theory]
    [InlineData(1969, 654)]
    [InlineData(100756, 33583)]
    public void Day01_Part01(int mass, int fuel)
    {
        var p = new Day01();
        p.OverrideInput(mass.ToString());
        Assert.Equal(fuel.ToString(), p.Part1());
    }

    [Theory]
    [InlineData(100756, 50346)]
    [InlineData(1969, 966)]
    public void Day01_Part02(int mass, int fuel)
    {
        var p = new Day01();
        p.OverrideInput(mass.ToString());
        Assert.Equal(fuel.ToString(), p.Part2());
    }
}