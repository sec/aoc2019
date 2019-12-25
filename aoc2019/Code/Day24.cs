using aoc2019.Misc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day24 : BaseDay
    {
        static readonly Dictionary<int, Eris> Planet = new Dictionary<int, Eris>();

        public Day24() : base(24)
        {
        }

        public class Eris
        {
            const int N = 5;

            private BitArray _eris, _temp;
            private readonly bool _multiLevel;
            private readonly int _level;

            public Eris(string data, bool isMultiLevel, int level)
            {
                _level = level;
                _multiLevel = isMultiLevel;

                _eris = new BitArray(N * N);
                _temp = new BitArray(N * N);

                if (!string.IsNullOrWhiteSpace(data))
                {
                    int i = 0;
                    foreach (var c in data)
                    {
                        if (c != '.' && c != '#')
                        {
                            continue;
                        }
                        _eris.Set(i++, c == '#');
                    }
                }
            }

            private int Count(int x, int y, bool what)
            {
                var c = 0;
                foreach (var m in Ext.GetNextMove(x, y))
                {
                    if (m.x < 0 || m.x >= N || m.y < 0 || m.y >= N)
                    {
                        var _prev = Planet.GetValueOrDefault(_level - 1);

                        if (_multiLevel && _prev != null)
                        {
                            // up
                            if (m.x < 0 && _prev.Get(1, 2))
                            {
                                c++;
                            }
                            // right
                            else if (m.x >= N && _prev.Get(3, 2))
                            {
                                c++;
                            }
                            // bottom
                            else if (m.y < 0 && _prev.Get(2, 1))
                            {
                                c++;
                            }
                            // left
                            else if (m.y >= N && _prev.Get(2, 3))
                            {
                                c++;
                            }
                        }
                    }
                    else if (Get(m.x, m.y) == what)
                    {
                        c++;
                    }
                }

                if (_multiLevel)
                {
                    c += CountNextLevel(x, y);
                }

                return c;
            }

            private int CountNextLevel(int x, int y)
            {
                var count = 0;

                var _next = Planet.GetValueOrDefault(_level + 1);
                if (_next == null)
                {
                    return count;
                }

                count += (x, y) switch
                {
                    // get 5 top rows from next level
                    (2, 1) => CountH(0, _next),

                    // get 5 right rows from next level
                    (3, 2) => CountV(N - 1, _next),

                    // get 5 bottom rows from next level
                    (2, 3) => CountH(N - 1, _next),

                    // get 5 left rows from next level
                    (1, 2) => CountV(0, _next),
                    _ => 0
                };

                return count;
            }

            public bool Get(int x, int y) => _eris.Get(y * N + x);

            void Set(int x, int y, bool what) => _temp.Set(y * N + x, what);

            public void Tick()
            {
                for (int y = 0; y < N; y++)
                {
                    for (int x = 0; x < N; x++)
                    {
                        if (_multiLevel && x == 2 && y == 2)
                        {
                            continue;
                        }

                        var count = Count(x, y, true);
                        var current = Get(x, y);

                        if (current && count != 1)
                        {
                            Set(x, y, false);
                        }
                        else if (current == false && (count == 1 || count == 2))
                        {
                            Set(x, y, true);
                        }
                        else
                        {
                            Set(x, y, current);
                        }
                    }
                }
            }

            public void Swap()
            {
                var tmp = _eris;
                _eris = _temp;
                _temp = tmp;
            }

            public int Rating()
            {
                int[] a = new int[1];
                _eris.CopyTo(a, 0);

                return a[0];
            }

            public int CountBugs() => _eris.OfType<bool>().Where(x => x).Count();

            private static int CountH(int y, Eris eris)
            {
                var c = 0;
                for (int i = 0; i < N; i++)
                {
                    if (eris.Get(i, y))
                    {
                        c++;
                    }
                }
                return c;
            }

            private static int CountV(int x, Eris eris)
            {
                var c = 0;
                for (int i = 0; i < N; i++)
                {
                    if (eris.Get(x, i))
                    {
                        c++;
                    }
                }
                return c;
            }
        }

        public override string Part1()
        {
            var eris = new Eris(ReadAllText(), false, 0);
            var set = new DefaultDict<int, int>();

            while (true)
            {
                var r = eris.Rating();
                set[r]++;

                if (set[r] > 1)
                {
                    return r.ToString();
                }
                eris.Tick();
                eris.Swap();
            }
        }

        public override string Part2()
        {
            foreach (var i in Enumerable.Range(-100, 201))
            {
                Planet[i] = new Eris(i == 0 ? ReadAllText() : string.Empty, true, i);
            }

            foreach (var n in Enumerable.Range(0, 200))
            {
                foreach (var e in Planet.Values)
                {
                    e.Tick();
                }
                foreach (var e in Planet.Values)
                {
                    e.Swap();
                }
            }

            return Planet.Values.Sum(x => x.CountBugs()).ToString();
        }
    }
}