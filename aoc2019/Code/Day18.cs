using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019.Code
{
    public class Day18 : BaseDay
    {
        class Map
        {
            char[,] _map;
            List<(int x, int y)> _starts;
            Dictionary<(int x, int y, string keys), int> _cache;

            readonly int _width, _height;

            public Map(string[] lines)
            {
                _cache = new Dictionary<(int x, int y, string keys), int>();
                _width = lines[0].Length;
                _height = lines.Length;
                _starts = new List<(int x, int y)>();

                _map = new char[_height, _width];

                for (var y = 0; y < _height; y++)
                {
                    for (var x = 0; x < _width; x++)
                    {
                        var c = lines[y][x];
                        _map[y, x] = c;
                        if (c == '@')
                        {
                            _starts.Add((x, y));
                        }
                    }
                }
            }

            static IEnumerable<(int x, int y)> GetNextMove(int x, int y)
            {
                yield return (x - 1, y);
                yield return (x + 1, y);
                yield return (x, y - 1);
                yield return (x, y + 1);
            }

            Dictionary<char, (int x, int y, int level)> WalkNext((int x, int y) start, string keysCollected)
            {
                var keys = new Dictionary<char, (int x, int y, int level)>();
                var dist = new Dictionary<(int x, int y), int>();
                var q = new Queue<(int x, int y)>();

                q.Enqueue(start);
                dist[start] = 0;

                while (q.Any())
                {
                    var current = q.Dequeue();

                    foreach (var move in GetNextMove(current.x, current.y))
                    {
                        var c = _map[move.y, move.x];

                        if (_map[move.y, move.x] == '#')
                        {
                            continue;
                        }
                        if (dist.ContainsKey(move))
                        {
                            continue;
                        }
                        dist[move] = dist[current] + 1;

                        if (char.IsUpper(c) && !keysCollected.Contains(char.ToLower(c)))
                        {
                            continue;
                        }
                        if (char.IsLower(c) && !keysCollected.Contains(c))
                        {
                            keys[c] = (move.x, move.y, dist[move]);
                        }
                        else
                        {
                            q.Enqueue(move);
                        }
                    }
                }
                return keys;
            }

            int Solve((int x, int y) start, string keys)
            {
                var sorted = string.Join(string.Empty, keys.OrderBy(x => x));

                if (_cache.TryGetValue((start.x, start.y, sorted), out var t))
                {
                    return t;
                }

                var steps = 0;
                var whatnext = WalkNext(start, sorted);
                if (whatnext.Any())
                {
                    var something = new List<int>();
                    foreach (var item in whatnext)
                    {
                        something.Add(item.Value.level + Solve((item.Value.x, item.Value.y), $"{sorted}{item.Key}"));
                    }
                    steps = something.Min();
                }
                _cache[(start.x, start.y, sorted)] = steps;

                return steps;
            }

            internal int Solve1() => Solve(_starts.First(), string.Empty);

            string GetKeysFromVault((int x, int y) start)
            {
                var keys = new StringBuilder();
                var visited = new HashSet<(int x, int y)>();
                var q = new Queue<(int x, int y)>();

                q.Enqueue(start);
                visited.Add(start);

                while (q.Any())
                {
                    var current = q.Dequeue();

                    foreach (var move in GetNextMove(current.x, current.y))
                    {
                        var c = _map[move.y, move.x];

                        if (_map[move.y, move.x] == '#')
                        {
                            continue;
                        }
                        if (visited.Contains(move))
                        {
                            continue;
                        }

                        if (char.IsLower(c))
                        {
                            keys.Append(c);
                        }

                        visited.Add(move);
                        q.Enqueue(move);

                    }
                }
                return keys.ToString();
            }

            internal int Solve2()
            {
                var v0 = GetKeysFromVault(_starts[0]);
                var v1 = GetKeysFromVault(_starts[1]);
                var v2 = GetKeysFromVault(_starts[2]);
                var v3 = GetKeysFromVault(_starts[3]);

                var solve0 = Solve(_starts[0], string.Join(string.Empty, v1, v2, v3).Trim());
                var solve1 = Solve(_starts[1], string.Join(string.Empty, v0, v2, v3).Trim());
                var solve2 = Solve(_starts[2], string.Join(string.Empty, v0, v1, v3).Trim());
                var solve3 = Solve(_starts[3], string.Join(string.Empty, v0, v1, v2).Trim());

                return solve0 + solve1 + solve2 + solve3;
            }
        }

        public Day18() : base(18)
        {
        }

        public override string Part1() => new Map(ReadAllLines()).Solve1().ToString();

        public override string Part2()
        {
            ReadInput("p2");

            return new Map(ReadAllLines()).Solve2().ToString();
        }
    }
}
