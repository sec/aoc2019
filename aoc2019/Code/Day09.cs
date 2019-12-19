using aoc2019.Misc;

namespace aoc2019.Code
{
    public class Day09 : BaseDay
    {
        public Day09() : base(9)
        {

        }

        public override string Part1() => Solve(1);

        public override string Part2() => Solve(2);

        private string Solve(long param)
        {
            long r = 0;

            var c = new IntCodeComputer(ReadAllText().ToIntCode());
            c.SignalOutput += _output => r = _output;
            c.AddInput(param);
            c.Run();

            return r.ToString();
        }
    }
}