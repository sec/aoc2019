using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day17 : BaseDay
    {
        public class Map
        {
            public enum MapType
            {
                Empty,
                Wall
            }

            readonly List<MapType> map = new List<MapType>();
            readonly IntCodeComputer c;

            public int MapHeight => map.Count / MapWidth;
            public int MapWidth { get; private set; }
            public int Robot { get; private set; }

            public Map(IEnumerable<long> code)
            {
                c = new IntCodeComputer(code);
                c.SignalOutput += o =>
                {
                    switch (o)
                    {
                        case 35:
                            map.Add(MapType.Wall);
                            break;

                        case 10:
                            if (MapWidth == 0)
                            {
                                MapWidth = map.Count;
                            }
                            break;

                        case 46:
                            map.Add(MapType.Empty);
                            break;

                        case 94:
                            Robot = map.Count;
                            map.Add(MapType.Wall);
                            break;

                        default:
                            throw new NotImplementedException(o.ToString());
                    }
                };
                c.Run();
            }

            public bool IsWall(int x, int y)
            {
                if (x < 0 || x >= MapWidth || y < 0 || y >= MapHeight)
                {
                    return false;
                }
                return map[y * MapWidth + x] == MapType.Wall;
            }

            public bool IsIntersect(int x, int y) => (IsWall(x - 1, y) && IsWall(x + 1, y)) && (IsWall(x, y - 1) && IsWall(x, y + 1));

            public MapType this[int x, int y]
            {
                get
                {
                    return map[y * MapWidth + x];
                }
            }

            public void ForEach(Action<(int x, int y), MapType> action)
            {
                foreach (var (x, y, t) in Each())
                {
                    action((x, y), t);
                }
            }

            public IEnumerable<(int x, int y, MapType t)> Each()
            {
                foreach (var i in Enumerable.Range(0, map.Count))
                {
                    var row = i / MapWidth;
                    var col = i % MapWidth;

                    yield return (col, row, this[col, row]);
                }
            }

            public void Print()
            {
                ForEach((_pos, _type) =>
                {
                    if (_pos.x == 0)
                    {
                        Console.WriteLine();
                    }

                    switch (_type)
                    {
                        case Map.MapType.Empty:
                            Console.Write('.');
                            break;
                        case Map.MapType.Wall:
                            var flag = IsIntersect(_pos.x, _pos.y);
                            var ry = Robot / MapWidth;
                            var rx = Robot % MapWidth;
                            if (_pos.x == rx && _pos.y == ry)
                            {
                                Console.Write('^');
                            }
                            else
                            {
                                Console.Write(flag ? 'O' : '#');
                            }
                            break;
                    }
                    Console.Write(' ');
                });

                Console.WriteLine();
            }
        }

        public Day17() : base(17)
        {

        }

        public override string Part1()
        {
            var map = new Map(ReadAllText().ToIntCode());

            return map.Each()
                .Where(p => p.t == Map.MapType.Wall && map.IsIntersect(p.x, p.y))
                .Select(p => p.x * p.y)
                .Sum()
                .ToString();
        }

        public override string Part2()
        {
            var ans = 0L;
            var code = new IntCodeComputer(ReadAllText().ToIntCode());
            code[0] = 2;

            var m = "A,B,A,B,C,C,B,A,C,A";
            var a = "L,10,R,8,R,6,R,10";
            var b = "L,12,R,8,L,12";
            var c = "L,10,R,8,R,8";

            var input = new Queue<long>();

            foreach (var i in new[] { m, a, b, c })
            {
                for (var j = 0; j < i.Length; j++)
                {
                    input.Enqueue(i[j]);
                }
                input.Enqueue(10);
            }
            input.Enqueue('n');
            input.Enqueue(10);

            code.SignalOutput += o => ans = o;
            code.ProvideInput += () => input.Dequeue();
            code.Run();

            return ans.ToString();
        }
    }
}