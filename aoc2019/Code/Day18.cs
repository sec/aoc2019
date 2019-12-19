using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019.Code
{
    public class Day18 : BaseDay
    {
        //TODO: rethink and make it "work"
        class Map
        {
            const int KEY_FOUND = 1;
            const int KEY_NOT_FOUND = -1;

            Dictionary<(int x, int y), char> _items;
            Dictionary<char, (int x, int y)> _items2;
            bool[,] _map;
            (int x, int y) _start;

            readonly int _width, _height;

            public Map(string[] lines)
            {
                _width = lines[0].Length;
                _height = lines.Length;

                _items = new Dictionary<(int x, int y), char>();
                _items2 = new Dictionary<char, (int x, int y)>();
                _map = new bool[_height, _width];

                for (var y = 0; y < _height; y++)
                {
                    for (var x = 0; x < _width; x++)
                    {
                        var c = lines[y][x];
                        switch (c)
                        {
                            case '#':
                                _map[y, x] = true;
                                break;

                            case '@':
                                _start = (x, y);
                                break;

                            case '.':
                                break;

                            default:
                                _items[(x, y)] = c;
                                _items2[c] = (x, y);
                                break;
                        }
                    }
                }
            }

            void Each(Action<(int x, int y), bool> func)
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        func((x, y), _map[y, x]);
                    }
                }
            }

            static Dictionary<string, int> _cacheCanWalk = new Dictionary<string, int>();

            int CanWalk((int x, int y) start, char itemToCollect, Dictionary<(int x, int y), char> keys, bool checkKeys)
            {
                var dkey = checkKeys ? $"{start}-{itemToCollect}-{string.Join(string.Empty, keys.Select(kv => kv.Value))}" : null;
                if (checkKeys && _cacheCanWalk.TryGetValue(dkey, out var r))
                {
                    return r;
                }

                var prev = new Dictionary<(int x, int y), (int x, int y)>();
                var flag = new HashSet<(int x, int y)>();
                var q = new Queue<(int x, int y)>();

                flag.Add(start);
                q.Enqueue(start);

                // perform BFS
                while (q.Count > 0)
                {
                    var v = q.Dequeue();

                    foreach (var w in GetNextMove(v.x, v.y))
                    {
                        if (!flag.Contains(w))
                        {
                            flag.Add(w);

                            if (_map[w.y, w.x] == true)
                            {
                                continue;
                            }

                            if (checkKeys)
                            {
                                // is there a key or door? treat our item as "empty" space                            
                                var item = keys.GetValueOrDefault(w);
                                if (item != 0 && item != itemToCollect)
                                {
                                    // that's not the key we're looking for, ignore
                                    continue;
                                }
                                if (item == itemToCollect)
                                {
                                    // our key was found
                                    _cacheCanWalk[dkey] = KEY_FOUND;
                                    return KEY_FOUND;
                                }
                            }

                            if (!checkKeys)
                            {
                                prev[w] = v;
                            }
                            q.Enqueue(w);
                        }
                    }
                }
                if (checkKeys)
                {
                    _cacheCanWalk[dkey] = KEY_NOT_FOUND;
                    return KEY_NOT_FOUND;
                }

                // find length of shortest path
                var end = _items2[itemToCollect];
                foreach (var step in Enumerable.Range(1, int.MaxValue))
                {
                    end = prev[end];

                    if (end == start)
                    {
                        return step;
                    }
                }

                throw new InvalidProgramException();
            }

            static IEnumerable<(int x, int y)> GetNextMove(int x, int y)
            {
                yield return (x - 1, y);
                yield return (x + 1, y);
                yield return (x, y - 1);
                yield return (x, y + 1);
            }

            IEnumerable<string> Bla(string path, Dictionary<(int x, int y), char> keys)
            {
                if (keys.Count == 0)
                {
                    yield return path;
                }

                foreach (var next in keys)
                {
                    char startPoint = path.LastOrDefault();
                    var startPos = startPoint == 0 ? _start : _items2[startPoint];

                    char nextPoint = next.Value;
                    var newPath = $"{path}{nextPoint}";

                    if (IsValidPath(newPath) && CanWalk(startPos, nextPoint, keys, true) == KEY_FOUND)
                    {
                        var newKeys = keys.ToDictionary(kv => kv.Key, kv => kv.Value);
                        newKeys.Remove(next.Key);

                        foreach (var item in Bla(newPath, newKeys))
                        {
                            yield return item;
                        }
                    }
                }
            }

            int GetPathLength(string path)
            {
                var start = _start;
                var length = 0;
                foreach (var checkpoint in path)
                {
                    length += CanWalk(start, checkpoint, null, false);
                    start = _items2[checkpoint];
                }
                return length;
            }

            static bool IsValidPath(string path)
            {
                foreach (var door in path.Where(x => char.IsUpper(x)))
                {
                    var key = path.IndexOf(char.ToLower(door));
                    if (key == -1 || key > door)
                    {
                        return false;
                    }

                }
                return true;
            }

            internal int Solve1()
            {
                var all = Bla(string.Empty, _items);
                var min = int.MaxValue;

                foreach (var i in all)
                {
                    var ans = GetPathLength(i);
                    if (ans < min)
                    {
                        min = ans;
                        Console.WriteLine($"Current min: {min}");
                    }
                }
                return min;
            }
        }

        public Day18() : base(18)
        {
        }

        public override string Part1() => new Map(ReadAllLines()).Solve1().ToString();

        public override string Part2()
        {
            return string.Empty;
        }
    }
}