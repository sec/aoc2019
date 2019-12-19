using aoc2019.Code;
using Xunit;

public class Day14UnitTests
{
    [Theory]
    [InlineData(485720)]
    public void Day14_Part1(long part1)
    {
        var d = new Day14();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(3848998)]
    public void Day14_Part2(long part2)
    {
        var d = new Day14();

        Assert.Equal(part2.ToString(), d.Part2());
    }

    const string INPUT1 = @"157 ORE => 5 NZVS
                    165 ORE => 6 DCFZ
                    44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
                    12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
                    179 ORE => 7 PSHF
                    177 ORE => 5 HKGWZ
                    7 DCFZ, 7 PSHF => 2 XJWVT
                    165 ORE => 2 GPVTF
                    3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT";

    const string INPUT2 = @"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
                            17 NVRVD, 3 JNWZP => 8 VPVL
                            53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
                            22 VJHF, 37 MNCFX => 5 FWMGM
                            139 ORE => 4 NVRVD
                            144 ORE => 7 JNWZP
                            5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
                            5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
                            145 ORE => 6 MNCFX
                            1 NVRVD => 8 CXFTF
                            1 VJHF, 6 MNCFX => 4 RFSQX
                            176 ORE => 6 VJHF";

    const string INPUT3 = @"171 ORE => 8 CNZTR
                            7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
                            114 ORE => 4 BHXH
                            14 VRPVC => 6 BMBT
                            6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
                            6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
                            15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
                            13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
                            5 BMBT => 4 WPTQ
                            189 ORE => 9 KTJDG
                            1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
                            12 VRPVC, 27 CNZTR => 2 XDBXC
                            15 KTJDG, 12 BHXH => 5 XCVML
                            3 BHXH, 2 VRPVC => 7 MZWV
                            121 ORE => 7 VRPVC
                            7 XCVML => 6 RJRHP
                            5 BHXH, 4 VRPVC => 5 LTCX";

    [Theory]
    [InlineData(INPUT1, 13312)]
    [InlineData(INPUT2, 180697)]
    [InlineData(INPUT3, 2210736)]
    public void Day14_Part1_Extra(string input, long part1)
    {
        var d = new Day14();
        d.OverrideInput(input);

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(INPUT1, 82892753)]
    [InlineData(INPUT2, 5586022)]
    [InlineData(INPUT3, 460664)]
    public void Day14_Part2_Extra(string input, long part2)
    {
        var d = new Day14();
        d.OverrideInput(input);

        Assert.Equal(part2.ToString(), d.Part2());
    }
}