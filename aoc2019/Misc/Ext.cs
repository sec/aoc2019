using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Misc
{
    public static class Ext
    {
        public static IEnumerable<long> ToIntCode(this string src)
        {
            return src.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
        }

        public static int ManhattanDistance(int x1, int x2, int y1, int y2) => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        public static IEnumerable<string> Split(this string src, char c)
        {
            return src.Split(new[] { c }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> Permutate(this string source)
        {
            if (source.Length == 1)
            {
                return new List<string> { source };
            }

            var permutations = from c in source
                               from p in Permutate(new string(source.Where(x => x != c).ToArray()))
                               select c + p;

            return permutations;
        }

        public static ulong NWW(ulong a, ulong b)
        {
            ulong ab = a * b;

            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }

            return ab / a;
        }

        public static ulong NWD(ulong a, ulong b)
        {
            while (a != b)
            {
                if (a < b)
                {
                    b -= a;
                }
                else
                {
                    a -= b;
                }
            }
            return a;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                {
                    yield return new T[] { item };
                }
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                    {
                        yield return new T[] { item }.Concat(result);
                    }
                }
                ++i;
            }
        }
    }
}