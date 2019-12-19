using aoc2019.Code;
using Xunit;

public class Day03UnitTests
{
    [Theory]
    [InlineData(2050, 21666)]
    public void Day03(int part1, int part2)
    {
        var d = new Day03();
        Assert.Equal(part1.ToString(), d.Part1());
        Assert.Equal(part2.ToString(), d.Part2());
    }

    [Theory]
    [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83", 159, 610)]
    [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135, 410)]
    public void Day03_Extra(string code, int part1, int part2)
    {
        var d = new Day03();
        d.OverrideInput(code);
        Assert.Equal(part1.ToString(), d.Part1());
        Assert.Equal(part2.ToString(), d.Part2());
    }
}