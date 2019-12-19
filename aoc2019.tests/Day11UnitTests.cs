using aoc2019.Code;
using System;
using Xunit;

public class Day11UnitTests
{
    [Theory]
    [InlineData(2268)]
    public void Day11_Part1(long part1)
    {
        var d = new Day11();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(" XX  XXXX XXX  X  X XXXX   XX  XX  XXX X  X X    X  X X X     X    X X  X X  XX    XXX  X  X XX     X     X X    X  XX    X    XXX  X X   X      X X    XXX X  X X    X    X X  X    X  X X  X X X  XX  XXXX X    X  X XXXX  XX   XX  X  X")]
    public void Day11_Part2(string cepkzjcr)
    {
        var d = new Day11();

        Assert.Equal(cepkzjcr.Trim(), d.Part2().Replace(Environment.NewLine, string.Empty).Trim());
    }
}