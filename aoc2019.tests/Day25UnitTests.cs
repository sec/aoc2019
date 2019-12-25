using aoc2019.Code;
using Xunit;

public class Day25UnitTests
{
    [Theory]
    [InlineData(229384)]
    public void Day25_Final(long ans)
    {
        var d = new Day25();

        Assert.Equal(ans.ToString(), d.Part2());
    }
}