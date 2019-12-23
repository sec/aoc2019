using aoc2019.Code;
using Xunit;

public class Day23UnitTests
{
    [Theory]
    [InlineData(24954, 17091)]
    public void Day23_Both_Parts(long part1, long part2)
    {
        var d = new Day23();

        Assert.Equal(part1.ToString(), d.Part1());
        Assert.Equal(part2.ToString(), d.Part2());
    }
}