using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day15 : BaseDay
    {
        public Day15() : base(15)
        {

        }

        enum MapType
        {
            Unknown = 0,
            Wall,
            Empty,
            Oxygen
        }

        enum DroidReponse
        {
            Wall = 0,
            Moved = 1,
            Oxygen = 2,
            Unknown = 3,
            Player = 4,
            Air = 5
        }

        enum Move
        {
            North = 1,
            South = 2,
            West = 3,
            East = 4
        }

        class Map
        {
            const int SIZE = 43;
            const int JUMP = SIZE / 2;

            DroidReponse[,] _map;
            (int x, int y) _oxygen = (0, 0);

            public Map()
            {
                _map = new DroidReponse[SIZE, SIZE];

                foreach (var i in Enumerable.Range(0, SIZE))
                {
                    foreach (var j in Enumerable.Range(0, SIZE))
                    {
                        _map[i, j] = DroidReponse.Unknown;
                    }
                }
            }

            public DroidReponse Get(int x, int y) => _map[y + JUMP, x + JUMP];

            public void Set(int x, int y, DroidReponse c)
            {
                if (Get(x, y) == DroidReponse.Unknown)
                {
                    _map[y + JUMP, x + JUMP] = c;
                    
                    // save this for later
                    if (c == DroidReponse.Oxygen)
                    {
                        _oxygen = (x + JUMP, y + JUMP);
                    }
                }
            }

            public void Print()
            {
                foreach (var i in Enumerable.Range(0, SIZE))
                {
                    foreach (var j in Enumerable.Range(0, SIZE))
                    {
                        Console.Write(Put(_map[i, j]));
                    }
                    Console.WriteLine();
                }

                static char Put(DroidReponse c)
                {
                    return c switch
                    {
                        DroidReponse.Moved => ' ',
                        DroidReponse.Oxygen => '*',
                        DroidReponse.Wall => '#',
                        DroidReponse.Unknown => '?',
                        DroidReponse.Player => '@',
                        DroidReponse.Air => 'O',
                        _ => throw new InvalidProgramException()
                    };
                }
            }

            internal int WalkFromStartToEnd()
            {
                var start = (JUMP, JUMP);

                var prev = new Dictionary<(int x, int y), (int x, int y)>();
                var flag = new HashSet<(int x, int y)>();
                var q = new Queue<(int x, int y)>();

                flag.Add(start);
                q.Enqueue(start);

                // perform BFS
                while (q.Count > 0)
                {
                    var v = q.Dequeue();
                    foreach (var w in GetNext(v.x, v.y))
                    {
                        if (!flag.Contains(w))
                        {
                            flag.Add(w);
                            prev[w] = v;
                            q.Enqueue(w);
                        }
                    }
                }

                // find length of shortest path
                var end = _oxygen;
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

            IEnumerable<(int x, int y)> GetNext(int x, int y)
            {
                foreach (var i in new (int x, int y)[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    (int x, int y) next = (x + i.x, y + i.y);

                    if (new[] { DroidReponse.Moved, DroidReponse.Oxygen }.Contains(_map[next.y, next.x]))
                    {
                        yield return next;
                    }
                }
            }

            internal int Floodfill()
            {
                // run the air
                _map[_oxygen.y, _oxygen.x] = DroidReponse.Air;

                var q = new Queue<(int x, int y)>();
                AddOxygen(q, _oxygen);

                int steps = 0;
                while (q.Count > 0)
                {
                    var tmp = new Queue<(int x, int y)>();
                    while (q.Count > 0)
                    {
                        var i = q.Dequeue();
                        _map[i.y, i.x] = DroidReponse.Air;

                        AddOxygen(tmp, i);
                    }

                    steps++;

                    // room is full
                    if (tmp.Count == 0)
                    {
                        return steps;
                    }

                    while (tmp.Count > 0)
                    {
                        q.Enqueue(tmp.Dequeue());
                    }
                }

                return steps;

                void AddOxygen(Queue<(int x, int y)> list, (int x, int y) point)
                {
                    foreach (var n in GetNext(point.x, point.y))
                    {
                        list.Enqueue(n);
                    }
                }
            }
        }

        Map GoMap(Action<Map> oxygenFound, Func<Map, bool> shouldEnd)
        {
            var map = new Map();
            var c = new IntCodeComputer(ReadAllText().ToIntCode());
            var r = new Random();

            (int x, int y) currentPos = (0, 0);
            (int x, int y) nextMove = (0, 0);

            c.SignalOutput += o =>
            {
                var resp = (DroidReponse) o;
                map.Set(nextMove.x, nextMove.y, resp);

                if (resp == DroidReponse.Moved || resp == DroidReponse.Oxygen)
                {
                    currentPos = nextMove;
                }

                if (resp == DroidReponse.Oxygen)
                {
                    oxygenFound(map);
                }
            };

            c.ProvideInput += () =>
            {
                if (shouldEnd(map))
                {
                    return -1;
                }
                var d = r.Next(1, 5);
                nextMove = ToXY((Move) d);

                return d;
            };

            c.Run();

            return map;
            //
            (int x, int y) ToXY(Move d)
            {
                return d switch
                {
                    Move.North => (currentPos.x, currentPos.y - 1),
                    Move.South => (currentPos.x, currentPos.y + 1),
                    Move.West => (currentPos.x - 1, currentPos.y),
                    Move.East => (currentPos.x + 1, currentPos.y),
                    _ => throw new NotImplementedException()
                };
            }
        }

        public override string Part1()
        {
            var ans = 0;
            GoMap(_map =>
            {
                ans = _map.WalkFromStartToEnd();
            }, _ => ans > 0);

            return ans.ToString();
        }

        public override string Part2()
        {
            var c = 2_000_000;
            return GoMap(_ => { }, _map => c-- < 0).Floodfill().ToString();
        }
    }
}