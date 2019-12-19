using aoc2019.Misc;

namespace aoc2019.Code
{
    public class Day05 : BaseDay
    {
        public Day05() : base(5)
        {

        }

        string Solve(int input)
        {
            var r = string.Empty;

            var c = new IntCodeComputer(ReadAllText().ToIntCode());
            c.AddInput(input);
            c.SignalOutput += _o => r = _o.ToString();
            c.Run();

            return r;
        }

        public override string Part1() => Solve(1);

        public override string Part2() => Solve(5);
    }
}