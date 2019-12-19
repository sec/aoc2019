using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace aoc2019.Code
{
    public class Day11 : BaseDay
    {
        const int SIZE = 120;

        enum Direction { up, down, left, right };

        IntCodeComputer _intComp;
        Direction _direction;
        int _rx, _ry;
        int[,] _map;
        HashSet<(int x, int y)> cnt = new HashSet<(int x, int y)>();

        void Set(int v) => _map[_ry + SIZE / 2, _rx + SIZE / 2] = v;
        int Get() => _map[_ry + SIZE / 2, _rx + SIZE / 2];

        public Day11() : base(11)
        {

        }

        void Init()
        {
            _intComp = new IntCodeComputer(ReadAllText().ToIntCode());

            _direction = Direction.up;
            _rx = _ry = 0;
            _map = new int[SIZE, SIZE];
        }

        void Paint()
        {
            var flag = true;

            _intComp.SignalOutput += _output =>
            {
                if (flag)
                {
                    Set((int) _output);
                    if (_output == 1)
                    {
                        cnt.Add((_rx, _ry));
                    }
                }
                else
                {
                    if (_output == 0)
                    {
                        // go left
                        switch (_direction)
                        {
                            case Direction.down:
                                _direction = Direction.right;
                                _rx++;
                                break;
                            
                            case Direction.up:
                                _direction = Direction.left;
                                _rx--;
                                break;
                            
                            case Direction.left:
                                _direction = Direction.down;
                                _ry++;
                                break;
                            
                            case Direction.right:
                                _direction = Direction.up;
                                _ry--;
                                break;
                        }

                    }
                    else if (_output == 1)
                    {
                        // go right
                        switch (_direction)
                        {
                            case Direction.up:
                                _direction = Direction.right;
                                _rx++;
                                break;
                            
                            case Direction.down:
                                _direction = Direction.left;
                                _rx--;
                                break;
                            
                            case Direction.left:
                                _direction = Direction.up;
                                _ry--;
                                break;
                            
                            case Direction.right:
                                _direction = Direction.down;
                                _ry++;
                                break;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                flag = !flag;
            };

            _intComp.ProvideInput += () =>
            {
                return Get();
            };

            _intComp.Run();
        }


        public override string Part1()
        {
            Init();
            Paint();

            return cnt.Count.ToString();
        }

        public override string Part2()
        {
            int sx, sy, ex, ey;
            sx = sy = SIZE;
            ex = ey = 0;

            Init();
            Set(1);
            Paint();

            for (int y = 0; y < SIZE; y++)
            {
                for (int x = 0; x < SIZE; x++)
                {
                    if (_map[y, x] != 0)
                    {
                        sx = Math.Min(sx, x);
                        sy = Math.Min(sy, y);
                        ex = Math.Max(ex, x);
                        ey = Math.Max(ey, y);
                    }
                }
            }

            var sb = new StringBuilder();
            for (int y = sy; y <= ey; y++)
            {
                for (int x = sx; x <= ex; x++)
                {
                    sb.Append(_map[y, x] == 0 ? ' ' : 'X');
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}