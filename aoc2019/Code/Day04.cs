using aoc2019.Misc;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day04 : BaseDay
    {
        private readonly int _min, _max;

        public Day04() : base(4)
        {
            var d = ReadAllTextSplit("-");
            _min = int.Parse(d[0]);
            _max = int.Parse(d[1]);
        }

        public static bool IsValid(int n)
        {
            var s = n.ToString();
            var flag = false;

            for (var i = 1; i < 6; i++)
            {
                var p = s[i - 1];
                var a = s[i];
                if (a < p)
                {
                    return false;
                }
                if (a == p)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool IsValidTwo(int n)
        {
            var s = n.ToString().ToCharArray().GroupBy(x => x);

            if (s.Any(g => g.Count() == 2))
            {
                return true;
            }

            return false;
        }

        IEnumerable<int> Solve() => Enumerable.Range(_min, _max - _min).Where(IsValid);

        public override string Part1() => Solve().Count().ToString();

        public override string Part2() => Solve().Where(IsValidTwo).Count().ToString();
    }
}