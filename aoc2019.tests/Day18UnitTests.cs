using aoc2019.Code;
using Xunit;

public class Day18UnitTests
{
    [Theory]
    [InlineData(3512)]
    public void Day18_Part1(long ans)
    {
        var d = new Day18();

        Assert.Equal(ans.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(1514)]
    public void Day18_Part2(long ans)
    {
        var d = new Day18();

        Assert.Equal(ans.ToString(), d.Part2());
    }

    [Theory]
    [InlineData(Input4, 32)]
    public void Day18_Part2_Extra(string input, long part1)
    {
        var d = new Day18();
        d.OverrideInput(input.Replace(" ", string.Empty));

        Assert.Equal(part1.ToString(), d.Part2());
    }

    [Theory]
    [InlineData(Input0, 86)]
    [InlineData(Input1, 132)]
    [InlineData(Input2, 136)]
    [InlineData(Input3, 81)]
    public void Day18_Part1_Extra(string input, long part1)
    {
        var d = new Day18();
        d.OverrideInput(input.Replace(" ", string.Empty));

        Assert.Equal(part1.ToString(), d.Part1());
    }

    const string Input0 = @"########################
                            #f.D.E.e.C.b.A.@.a.B.c.#
                            ######################.#
                            #d.....................#
                            ########################";

    const string Input1 = @"########################
                            #...............b.C.D.f#
                            #.######################
                            #.....@.a.B.c.d.A.e.F.g#
                            ########################";

    const string Input2 = @"#################
                            #i.G..c...e..H.p#
                            ########.########
                            #j.A..b...f..D.o#
                            ########@########
                            #k.E..a...g..B.n#
                            ########.########
                            #l.F..d...h..C.m#
                            #################";

    const string Input3 = @"########################
                            #@..............ac.GI.b#
                            ###d#e#f################
                            ###A#B#C################
                            ###g#h#i################
                            ########################";

    const string Input4 = @"#############
                            #DcBa.#.GhKl#
                            #.###@#@#I###
                            #e#d#####j#k#
                            ###C#@#@###J#
                            #fEbA.#.FgHi#
                            #############";
}