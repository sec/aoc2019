using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019.Code
{
    public class Day20 : BaseDay
    {
        public Day20() : base(20)
        {
        }

        class Donut
        {
            const int TELEPORT_MARK = 128;

            readonly int _teleportCounter = 0;
            readonly int _width;
            readonly int _height;
            readonly char[,] _map;
            readonly Dictionary<string, (int index, List<(int x, int y, bool outer)> waypoints)> _teleports;

            readonly char[] _solid = new[] { '#', ' ' };

            (int x, int y, int level) _start, _end;

            public Donut(string[] data)
            {
                _width = data[0].Length;
                _height = data.Length;
                _map = new char[_height, _width];
                _teleports = new Dictionary<string, (int index, List<(int x, int y, bool outer)> waypoints)>();

                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        var c = data[y][x];
                        if (char.IsUpper(c))
                        {
                            // get waypoint
                            var point = Ext.GetNextMove(x, y).Where(pos => InsideMap(pos) && data[pos.y][pos.x] == '.');
                            if (!point.Any())
                            {
                                // no waypoint around, teleport "other" side
                                continue;
                            }

                            // get second teleport name
                            var second = Ext.GetNextMove(x, y).Where(pos => InsideMap(pos) && char.IsUpper(data[pos.y][pos.x])).Single();
                            var secondChar = data[second.y][second.x];

                            var name = $"{c}{secondChar}";
                            if (secondChar < c)
                            {
                                name = $"{secondChar}{c}";
                            }

                            if (!_teleports.ContainsKey(name))
                            {
                                _teleports[name] = (_teleportCounter++, new List<(int x, int y, bool outer)>());
                            }

                            var pt = point.Single();
                            var outer = pt.x == 2 || pt.y == 2 || pt.x == _width - 3 || pt.y == _height - 3;
                            _teleports[name].waypoints.Add((pt.x, pt.y, outer));

                            if (name == "AA" || name == "ZZ")
                            {
                                _map[y, x] = _solid.First();
                                if (name == "AA")
                                {
                                    _start = (pt.x, pt.y, 0);
                                }
                                else
                                {
                                    _end = (pt.x, pt.y, 0);
                                }
                            }
                            else
                            {
                                _map[y, x] = (char) (TELEPORT_MARK + _teleports[name].index);
                            }
                        }
                        else
                        {
                            _map[y, x] = c;
                        }
                    }
                }

                _teleports.Remove("AA");
                _teleports.Remove("ZZ");
            }

            bool InsideMap((int x, int y) pos) => !(pos.x < 0 || pos.x >= _width || pos.y < 0 || pos.y >= _height);

            int BFS((int x, int y) start)
            {
                var visited = new Dictionary<(int x, int y), (int x, int y)>();
                var q = new Queue<(int x, int y)>();

                q.Enqueue(start);

                while (q.Any())
                {
                    var current = q.Dequeue();
                    var nextMoves = Ext.GetNextMove(current.x, current.y).ToList();

                    foreach (var teleport in _teleports)
                    {
                        var one = teleport.Value.waypoints.First();
                        var two = teleport.Value.waypoints.Last();

                        if ((one.x, one.y) == current)
                        {
                            nextMoves.Add((two.x, two.y));
                        }
                        else if ((two.x, two.y) == current)
                        {
                            nextMoves.Add((one.x, one.y));
                        }
                    }

                    foreach (var w in nextMoves)
                    {
                        if (!visited.ContainsKey(w))
                        {
                            visited[w] = current;

                            var c = _map[w.y, w.x];
                            if (_solid.Contains(c) || c >= TELEPORT_MARK)
                            {
                                continue;
                            }

                            q.Enqueue(w);
                        }
                    }
                }

                var step = 1;
                var end = (_end.x, _end.y);

                while (true)
                {
                    end = visited[end];

                    if (end == start)
                    {
                        return step;
                    }
                    step++;
                }
            }

            private void AddTeleport(List<(int x, int y, int level)> nextMoves, int level, (int x, int y) current)
            {
                // inner are active and give +1
                foreach (var teleport in _teleports)
                {
                    var one = teleport.Value.waypoints.First();
                    var two = teleport.Value.waypoints.Last();

                    if (one.outer == false && (one.x, one.y) == current)
                    {
                        nextMoves.Add((two.x, two.y, level + 1));
                    }
                    if (two.outer == false && (two.x, two.y) == current)
                    {
                        nextMoves.Add((one.x, one.y, level + 1));
                    }
                }

                if (level == 0)
                {
                    if ((_end.x, _end.y) == current)
                    {
                        nextMoves.Add(_end);
                    }
                }
                else
                {
                    foreach (var teleport in _teleports)
                    {
                        var one = teleport.Value.waypoints.First();
                        var two = teleport.Value.waypoints.Last();

                        if (one.outer && (one.x, one.y) == current)
                        {
                            nextMoves.Add((two.x, two.y, level - 1));
                        }
                        if (two.outer && (two.x, two.y) == current)
                        {
                            nextMoves.Add((one.x, one.y, level - 1));
                        }
                    }
                }
            }

            int BFS2((int x, int y) start, int startLevel)
            {
                var flag = new HashSet<(int x, int y, int level)>();
                var q = new Queue<(int x, int y, int level)>();
                var prev = new Dictionary<(int x, int y, int level), (int x, int y, int level)>();

                flag.Add((start.x, start.y, startLevel));
                q.Enqueue((start.x, start.y, startLevel));

                while (q.Count > 0)
                {
                    var current = q.Dequeue();

                    var nextMoves = Ext.GetNextMove(current.x, current.y).Select(pos => (pos.x, pos.y, current.level)).ToList();
                    AddTeleport(nextMoves, current.level, (current.x, current.y));

                    foreach (var w in nextMoves)
                    {
                        var c = _map[w.y, w.x];
                        if (_solid.Contains(c) || c >= TELEPORT_MARK)
                        {
                            continue;
                        }

                        if (!flag.Contains(w))
                        {
                            flag.Add(w);
                            prev[w] = current;
                            q.Enqueue(w);

                            if (w == _end)
                            {
                                var step = 1;
                                var track = _end;

                                while (true)
                                {
                                    track = prev[track];

                                    if (track == (start.x, start.y, startLevel))
                                    {
                                        return step;
                                    }
                                    step++;
                                }
                            }
                        }
                    }
                }

                throw new InvalidProgramException();
            }

            public int Part1() => BFS((_start.x, _start.y));

            public int Part2() => BFS2((_start.x, _start.y), _start.level);
        }

        public override string Part1() => new Donut(ReadAllLines()).Part1().ToString();

        public override string Part2() => new Donut(ReadAllLines()).Part2().ToString();
    }
}