using aoc2019.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace aoc2019.Code
{
    public class Day24 : BaseDay
    {
        public Day24() : base(24)
        {
        }

        class Eris
        {
            const int N = 5;

            BitArray _eris, _temp;

            public Eris(string data)
            {
                _eris = new BitArray(N * N);
                _temp = new BitArray(N * N);

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

            int Count(int x, int y, bool what)
            {
                var c = 0;
                foreach (var m in Ext.GetNextMove(x, y))
                {
                    if (m.x < 0 || m.x >= N || m.y < 0 || m.y >= N)
                    {
                        continue;
                    }
                    if (Get(m.x, m.y) == what)
                    {
                        c++;
                    }
                }
                return c;
            }

            bool Get(int x, int y) => _eris.Get(y * N + x);

            void Set(int x, int y, bool what) => _temp.Set(y * N + x, what);

            public void Tick()
            {
                for (int y = 0; y < N; y++)
                {
                    for (int x = 0; x < N; x++)
                    {
                        if (Get(x, y) && Count(x, y, true) != 1)
                        {
                            Set(x, y, false);
                        }
                        else if (Get(x, y) == false && (Count(x, y, true) == 1 || Count(x, y, true) == 2))
                        {
                            Set(x, y, true);
                        }
                        else
                        {
                            Set(x, y, Get(x, y));
                        }
                    }
                }
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
        }

        public override string Part1()
        {
            var eris = new Eris(ReadAllText());
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
            }
        }

        public override string Part2()
        {
            return string.Empty;
        }
    }
}