using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day10 : BaseDay
    {
        private readonly List<(int x, int y)> _points;
        private KeyValuePair<(int x, int y), Dictionary<double, List<(int x, int y)>>> _laserStand;

        static double MapKey(double rad) => rad > Math.PI / 2.0 ? rad - (2.0 * Math.PI) : rad;

        public Day10() : base(10)
        {
            _points = new List<(int x, int y)>();
            var data = ReadAllLines();

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][j] == '#')
                    {
                        _points.Add((j, i));
                    }
                }
            }
        }

        public override string Part1()
        {
            var dict = new Dictionary<(int x, int y), Dictionary<double, List<(int x, int y)>>>();

            foreach (var p1 in _points)
            {
                foreach (var p2 in _points)
                {
                    if (p1 == p2)
                    {
                        continue;
                    }
                    //
                    var angle = Math.Atan2(p1.y - p2.y, p1.x - p2.x);
                    if (dict.ContainsKey(p1) == false)
                    {
                        dict.Add(p1, new Dictionary<double, List<(int x, int y)>>());
                    }

                    if (!dict[p1].ContainsKey(angle))
                    {
                        dict[p1].Add(angle, new List<(int x, int y)>());
                    }
                    dict[p1][angle].Add(p2);
                }
            }

            var max = dict.Max(kv => kv.Value.Count);
            _laserStand = dict.Where(kv => kv.Value.Count == max).Single();

            return max.ToString();
        }

        public override string Part2()
        {
            var s = _laserStand.Key;
            var ast = _laserStand.Value.Select(kv => (MapKey(kv.Key), kv.Value)).OrderBy(x => x.Item1).ToList();
            int cnt = 0;
            while (cnt < 200)
            {
                foreach (var c in ast)
                {
                    if (c.Value.Any())
                    {
                        var closest = c.Value.OrderBy(x => Ext.ManhattanDistance(s.x, x.x, s.y, x.y)).First();
                        cnt++;
                        if (cnt == 199)
                        {
                            return (closest.x * 100 + closest.y).ToString();
                        }
                        c.Value.Remove(closest);
                    }
                }
            }

            throw new InvalidProgramException();
        }
    }
}