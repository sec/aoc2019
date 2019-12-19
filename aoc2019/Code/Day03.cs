using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aoc2019.Code
{
    struct Move
    {
        public char D;
        public int N;

        public Move(string input)
        {
            D = input[0];
            N = int.Parse(input.Substring(1));
        }
    }

    class Comparer : IEqualityComparer<(int x, int y, int total)>
    {
        public bool Equals([AllowNull] (int x, int y, int total) one, [AllowNull] (int x, int y, int total) two) => one.x == two.x && one.y == two.y;

        public int GetHashCode([DisallowNull] (int x, int y, int total) obj) => (obj.x, obj.y).GetHashCode();
    }

    public class Day03 : BaseDay
    {
        HashSet<(int x, int y, int total)> _moves1;
        HashSet<(int x, int y, int total)> _moves2;

        public Day03() : base(3)
        {
        }

        IEnumerable<(int x, int y, int total)> Solve()
        {
            var lines = ReadAllLines();

            var path1 = lines[0].Split(',').Select(x => new Move(x)).ToList();
            var path2 = lines[1].Split(',').Select(x => new Move(x)).ToList();

            _moves1 = MoveWire(path1).ToHashSet();
            _moves2 = MoveWire(path2).ToHashSet();

            return _moves1.Intersect(_moves2, new Comparer());
        }

        public override string Part1() => Solve().Min(x => Ext.ManhattanDistance(x.x, 0, x.y, 0)).ToString();

        public override string Part2()
        {
            var min = int.MaxValue;

            foreach (var item in Solve().ToList())
            {
                var wire1 = _moves1.Single(i => i.x == item.x && i.y == item.y);
                var wire2 = _moves2.Single(i => i.x == item.x && i.y == item.y);

                var total = wire1.total + wire2.total;

                if (total < min)
                {
                    min = total;
                }
            }

            return min.ToString();
        }

        static IEnumerable<(int x, int y, int total)> MoveWire(List<Move> moves)
        {
            var x = 0;
            var y = 0;
            var total = 0;

            foreach (var m in moves)
            {
                var i = m.N;
                while (i-- > 0)
                {
                    var (mx, my) = m.D switch
                    {
                        'U' => (0, -1),
                        'D' => (0, +1),
                        'R' => (+1, 0),
                        'L' => (-1, 0),
                        _ => throw new NotImplementedException()
                    };
                    x += mx;
                    y += my;
                    total += Math.Abs(mx);
                    total += Math.Abs(my);

                    yield return (x, y, total);
                }
            }
        }
    }
}