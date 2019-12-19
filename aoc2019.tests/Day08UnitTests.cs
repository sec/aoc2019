using aoc2019.Code;
using System;
using Xunit;

public class Day08UnitTests
{
    [Theory]
    [InlineData(1848)]
    public void Day08_Part1(long part1)
    {
        var d = new Day08();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData("1111  11    11 1  1 1111 1    1  1    1 1  1    1 111  1       1 1  1   1  1    1 11    1 1  1  1   1    1  1 1  1 1  1 1    1     111  11   11  1111")]
    public void Day08_Part2(string fgjuz)
    {
        var d = new Day08();

        Assert.Equal(fgjuz, d.Part2().Replace(Environment.NewLine, string.Empty));
    }
}