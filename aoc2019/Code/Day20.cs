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
            readonly int _teleportCounter = 0;
            readonly int _width;
            readonly int _height;
            readonly char[,] _map;
            readonly Dictionary<string, (int index, List<(int x, int y)> waypoints)> _teleports;

            bool InsideMap((int x, int y) pos) => !(pos.x < 0 || pos.x >= _width || pos.y < 0 || pos.y >= _height);

            public Donut(string[] data)
            {
                _width = data[0].Length;
                _height = data.Length;
                _map = new char[_height, _width];
                _teleports = new Dictionary<string, (int index, List<(int x, int y)> waypoints)>();

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
                                _teleports[name] = (_teleportCounter++, new List<(int x, int y)>());
                            }

                            _teleports[name].waypoints.Add(point.Single());

                            if (name == "AA")
                            {
                                _map[y, x] = '#';
                            }
                            else if (name == "ZZ")
                            {
                                _map[y, x] = 'X';
                            }
                            else
                            {
                                _map[y, x] = (char) (128 + _teleports[name].index);
                            }

                        }
                        else
                        {
                            _map[y, x] = c;
                        }
                    }
                }
            }

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

                        if (one == current)
                        {
                            nextMoves.Add(two);
                        }
                        else if (two == current)
                        {
                            nextMoves.Add(one);
                        }
                    }

                    foreach (var w in nextMoves)
                    {
                        if (!visited.ContainsKey(w))
                        {
                            visited[w] = current;

                            var c = _map[w.y, w.x];
                            if (c == '#' || c == ' ' || c == 'X' || c >= 128)
                            {
                                continue;
                            }

                            q.Enqueue(w);
                        }
                    }
                }

                var step = 1;
                var end = _teleports["ZZ"].waypoints.Single();

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

            public void Print()
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        Console.Write(_map[y, x]);
                    }
                    Console.WriteLine();
                }
            }

            public int Part1() => BFS(_teleports["AA"].waypoints.Single());
        }

        public override string Part1() => new Donut(ReadAllLines()).Part1().ToString();

        public override string Part2()
        {
            return string.Empty;
        }
    }
}