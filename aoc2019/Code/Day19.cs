using aoc2019.Misc;
using System.Collections.Generic;

namespace aoc2019.Code
{
    public class Day19 : BaseDay
    {
        readonly IntCodeComputer _c;
        readonly Queue<long> _input;
        long _output;

        const int SQAURE = 100;
        const int AREA = 50;

        bool IsLineDown(int x, int y) => GetOutput(x, y) && GetOutput(x, y + SQAURE - 1);
        bool IsLineRight(int x, int y) => GetOutput(x, y) && GetOutput(x + SQAURE - 1, y);

        public Day19() : base(19)
        {
            _c = new IntCodeComputer(ReadAllText().ToIntCode());
            _c.SignalOutput += _o => _output = _o;
            _c.ProvideInput += () => _input.Dequeue();
            _input = new Queue<long>();
        }

        bool GetOutput(int x, int y)
        {
            _input.Enqueue(x);
            _input.Enqueue(y);
            _c.Reset();
            _c.Run();

            return _output == 1;
        }

        public override string Part1()
        {
            var ans = 0L;
            for (int x = 0; x < AREA; x++)
            {
                for (int y = 0; y < AREA; y++)
                {
                    ans += GetOutput(x, y) ? 1 : 0;
                }
            }

            return ans.ToString();
        }

        public override string Part2()
        {
            // values taken by looking at beam output :)
            for (int y = 1550; y < int.MaxValue; y++)
            {
                for (int x = y; x < y + 4 * SQAURE; x++)
                {
                    if (!IsLineRight(x, y) || !IsLineDown(x, y))
                    {
                        continue;
                    }

                    return ((x) * 10000 + y).ToString();

                }
            }

            return string.Empty;
        }
    }
}