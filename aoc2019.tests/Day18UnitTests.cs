using aoc2019.Code;
using Xunit;

public class Day18UnitTests
{
    [Theory]
    [InlineData(Input0, 86)]
    [InlineData(Input1, 132)]
    //[InlineData(Input2, 136)]
    [InlineData(Input3, 81)]
    public void Day18_Part1(string input, long part1)
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
}