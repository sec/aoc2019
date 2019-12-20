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
            bool USE_SECOND_VERSION = false;

            char[,] _map;
            List<(int x, int y)> _starts;
            Dictionary<(int x, int y, string keys), int> _cache;
            List<char> _allKeys;

            readonly int _width, _height;

            public Map(string[] lines)
            {
                _cache = new Dictionary<(int x, int y, string keys), int>();
                _width = lines[0].Length;
                _height = lines.Length;
                _starts = new List<(int x, int y)>();
                _allKeys = new List<char>();
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
                        if (char.IsLower(c))
                        {
                            _allKeys.Add(c);
                        }
                    }
                }
            }

            #region Part 1
            /// <summary>
            /// 
            /// </summary>
            /// <param name="start"></param>
            /// <param name="keysCollected"></param>
            /// <returns></returns>
            Dictionary<char, (int x, int y, int level)> CollectNext((int x, int y) start, string keysCollected)
            {
                var keys = new Dictionary<char, (int x, int y, int level)>();
                var dist = new Dictionary<(int x, int y), int>();
                var q = new Queue<(int x, int y)>();

                q.Enqueue(start);
                dist[start] = 0;

                while (q.Any())
                {
                    var current = q.Dequeue();

                    foreach (var move in Ext.GetNextMove(current.x, current.y))
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
                var whatnext = CollectNext(start, sorted);
                if (whatnext.Any())
                {
                    steps = int.MaxValue;
                    foreach (var item in whatnext)
                    {
                        var d = (item.Value.level + Solve((item.Value.x, item.Value.y), $"{sorted}{item.Key}"));
                        steps = Math.Min(d, steps);
                    }
                }
                _cache[(start.x, start.y, sorted)] = steps;

                return steps;
            }
            #endregion

            #region Part 1 - Second version
            /// <summary>
            /// Travel around maze and collect keys
            /// </summary>
            /// <param name="start">Start point</param>
            /// <param name="keys">List of keys we need to collect</param>
            /// <returns>List of collected keys + coords</returns>
            Dictionary<char, (int x, int y)> WalkAndCollect((int x, int y) start, List<char> keys)
            {
                var k = new Dictionary<char, (int x, int y)>();
                var flag = new HashSet<(int x, int y)>();
                var q = new Queue<(int x, int y)>();

                flag.Add(start);
                q.Enqueue(start);

                while (q.Count > 0)
                {
                    var (x, y) = q.Dequeue();
                    foreach (var w in Ext.GetNextMove(x, y))
                    {
                        if (!flag.Contains(w))
                        {
                            flag.Add(w);

                            var c = _map[w.y, w.x];
                            if (c == '#')
                            {
                                continue;
                            }

                            // there's a door, but key for it are on our "to collect" list
                            if (char.IsUpper(c) && keys.Contains(char.ToLower(c)))
                            {
                                continue;
                            }

                            // there's a key and it's on our "collect list"
                            if (char.IsLower(c) && keys.Contains(c))
                            {
                                k[c] = w;
                            }
                            else
                            {
                                q.Enqueue(w);
                            }
                        }
                    }
                }
                return k;
            }

            int CollectKeys((int x, int y) position, List<char> keys)
            {
                if (keys.Count == 0)
                {
                    return 0;
                }

                var cacheKey = (position.x, position.y, string.Join(string.Empty, keys));
                if (_cache.ContainsKey(cacheKey))
                {
                    return _cache[cacheKey];
                }

                var ans = int.MaxValue;
                var reach = WalkAndCollect(position, keys);

                foreach (var item in reach)
                {
                    var newkeys = keys.Where(x => x != item.Key).ToList();
                    var d = LengthBetween(position, item.Value) + CollectKeys(item.Value, newkeys);
                    ans = Math.Min(ans, d);
                }

                _cache[cacheKey] = ans;
                return ans;
            }

            int LengthBetween((int x, int y) start, (int x, int y) end)
            {
                var prev = new Dictionary<(int x, int y), (int x, int y)>();
                var flag = new HashSet<(int x, int y)>();
                var q = new Queue<(int x, int y)>();

                flag.Add(start);
                q.Enqueue(start);

                // perform BFS
                while (q.Count > 0)
                {
                    var v = q.Dequeue();

                    foreach (var w in Ext.GetNextMove(v.x, v.y))
                    {
                        // can't walk into wall
                        if (_map[w.y, w.x] == '#')
                        {
                            continue;
                        }

                        if (!flag.Contains(w))
                        {
                            flag.Add(w);
                            prev[w] = v;
                            q.Enqueue(w);
                        }
                    }
                }

                var step = 1;
                while (true)
                {
                    end = prev[end];

                    if (end == start)
                    {
                        return step;
                    }
                    step++;
                }
            }

            internal int Solve1Second() => CollectKeys(_starts[0], _allKeys);
            #endregion

            #region Part 2
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

                    foreach (var move in Ext.GetNextMove(current.x, current.y))
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
            #endregion

            internal int Solve1() => USE_SECOND_VERSION ? CollectKeys(_starts[0], _allKeys) : Solve(_starts.First(), string.Empty);

            internal int Solve2()
            {
                int solve0, solve1, solve2, solve3;
                var keys = _starts.Select(GetKeysFromVault).ToList();
                var v0 = keys[0];
                var v1 = keys[1];
                var v2 = keys[2];
                var v3 = keys[3];

                if (USE_SECOND_VERSION)
                {
                    solve0 = CollectKeys(_starts[0], v0.ToList());
                    solve1 = CollectKeys(_starts[1], v1.ToList());
                    solve2 = CollectKeys(_starts[2], v2.ToList());
                    solve3 = CollectKeys(_starts[3], v3.ToList());
                }
                else
                {
                    solve0 = Solve(_starts[0], string.Join(string.Empty, v1, v2, v3));
                    solve1 = Solve(_starts[1], string.Join(string.Empty, v0, v2, v3));
                    solve2 = Solve(_starts[2], string.Join(string.Empty, v0, v1, v3));
                    solve3 = Solve(_starts[3], string.Join(string.Empty, v0, v1, v2));
                }

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