using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day12 : BaseDay
    {
        public class Moon
        {
            public int[] Pos, Vel;

            public Moon(int x, int y, int z)
            {
                Pos = new int[] { x, y, z };
                Vel = new int[] { 0, 0, 0 };
            }

            internal void Move()
            {
                for (int i = 0; i < 3; i++)
                {
                    Pos[i] += Vel[i];
                }
            }

            public int Kin => Math.Abs(Vel[0]) + Math.Abs(Vel[1]) + Math.Abs(Vel[2]);
            public int Pot => Math.Abs(Pos[0]) + Math.Abs(Pos[1]) + Math.Abs(Pos[2]);
            public int Energy => Kin * Pot;
        }

        public Day12() : base(12)
        {

        }

        public override string Part1()
        {
            var moons = GetMoons().ToList();
            var pairs = Ext.GetPermutations(moons, 2).Select(x => (x.First(), x.Last())).ToList();
            var steps = 1000;

            while (steps-- > 0)
            {
                Step(moons, pairs);
            }

            return moons.Select(x => x.Energy).Sum().ToString();
        }

        public override string Part2()
        {
            var init = GetMoons().ToList();
            var moons = GetMoons().ToList();
            var pairs = Ext.GetPermutations(moons, 2).Select(x => (x.First(), x.Last())).ToList();
            var cycles = new ulong[3];

            ulong steps = 0;
            while (true)
            {
                steps++;
                Step(moons, pairs);

                for (var j = 0; j < 3; j++)
                {
                    var ok = true;
                    foreach (var i in Enumerable.Range(0, moons.Count))
                    {
                        if (!(moons[i].Pos[j] == init[i].Pos[j] && moons[i].Vel[j] == 0 && cycles[j] == 0))
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                    {
                        cycles[j] = steps;
                    }
                }

                if (cycles.All(x => x != 0))
                {
                    return Ext.NWW(cycles[0], Ext.NWW(cycles[1], cycles[2])).ToString();
                }
            }
        }

        IEnumerable<Moon> GetMoons()
        {
            foreach (var line in ReadAllLinesSplit(","))
            {
                yield return new Moon(
                    x: int.Parse(line[0].Substring(3)),
                    y: int.Parse(line[1].Substring(3)),
                    z: int.Parse(line[2].Trim('>').Substring(3))
                );
            }
        }

        static void Step(List<Moon> moons, List<(Moon, Moon)> pairs)
        {
            foreach (var pair in pairs)
            {
                var a = pair.Item1;
                var b = pair.Item2;

                for (int p = 0; p < 3; p++)
                {
                    if (a.Pos[p] < b.Pos[p])
                    {
                        a.Vel[p]++;
                        b.Vel[p]--;
                    }
                    else if (a.Pos[p] > b.Pos[p])
                    {
                        a.Vel[p]--;
                        b.Vel[p]++;
                    }
                }
            }
            moons.ForEach(x => x.Move());
        }
    }
}