using aoc2019.Misc;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day06 : BaseDay
    {
        Dictionary<string, string> GetMap() => ReadAllLinesSplit(")").ToDictionary(k => k[1], v => v[0]);

        public Day06() : base(6)
        {

        }

        public override string Part1()
        {
            var map = GetMap();

            return map.Keys.Select(x => Step(map, x)).Sum().ToString();
        }

        public override string Part2() => (BFS(GetMap(), "YOU", "SAN") - 1).ToString();

        int Step(Dictionary<string, string> map, string root) => map.ContainsKey(root) ? Step(map, map[root]) + 1 : 0;

        int BFS(Dictionary<string, string> map, string root, string end)
        {
            var q = new Queue<string>();
            var disc = new HashSet<string>();
            var prev = new Dictionary<string, string>();

            disc.Add(root);
            q.Enqueue(root);

            while (q.Count > 0)
            {
                var v = q.Dequeue();
                foreach (var w in GetOrbitsFor(map, v))
                {
                    if (!disc.Contains(w))
                    {
                        disc.Add(w);
                        q.Enqueue(w);
                        prev[w] = v;
                    }
                }
            }

            var step = 0;
            var track = end;
            while (true)
            {
                track = prev[track];

                if (track == root)
                {
                    return step;
                }

                step++;
            }
        }

        IEnumerable<string> GetOrbitsFor(Dictionary<string, string> map, string v) => map.Where(x => x.Key == v)
            .Select(x => x.Value)
            .Union(map.Where(x => x.Value == v)
            .Select(x => x.Key));
    }
}