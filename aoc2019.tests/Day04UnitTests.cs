using aoc2019.Code;
using Xunit;

public class Day04UnitTests
{
    [Theory]
    [InlineData(895, 591)]
    public void Day04_Both(int part1, int part2)
    {
        Day04 p = new Day04();

        Assert.Equal(part1.ToString(), p.Part1());
        Assert.Equal(part2.ToString(), p.Part2());
    }

    [Theory]
    [InlineData(111111, true)]
    [InlineData(223450, false)]
    [InlineData(123789, false)]
    public void Day04_Part1(int input, bool valid)
    {
        Assert.Equal(valid, Day04.IsValid(input));
    }

    [Theory]
    [InlineData(112233, true)]
    [InlineData(123444, false)]
    [InlineData(111122, true)]
    public void Day04_Part2(int input, bool valid)
    {
        Assert.Equal(valid, Day04.IsValidTwo(input));
    }
}