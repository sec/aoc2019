using aoc2019.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019.Code
{
    public class Day08 : BaseDay
    {
        const int WIDTH = 25;
        const int HEIGHT = 6;
        const char WHITE = '1';
        const char TRANS = '2';

        private readonly char[] _input;
        readonly List<StringBuilder> _layers;

        public Day08() : base(8)
        {
            _layers = new List<StringBuilder>();
            _input = ReadAllText().ToCharArray();

            int layer = 0;
            _layers.Add(new StringBuilder());

            foreach (var c in _input)
            {
                if (_layers[layer].Length >= WIDTH * HEIGHT)
                {
                    layer++;
                    _layers.Add(new StringBuilder());
                }
                _layers[layer].Append(c);
            }
        }

        public override string Part1()
        {
            return (_layers.Select(x => new
            {
                ZeroCount = x.ToString().Where(x => x == '0').Count(),
                Part1 = x.ToString().Where(x => x == '1').Count() * x.ToString().Where(x => x == '2').Count()
            }).OrderBy(x => x.ZeroCount).First().Part1).ToString();
        }

        public override string Part2()
        {
            var image = new char[HEIGHT, WIDTH];
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    image[y, x] = TRANS;
                }
            }

            foreach (var layer in _layers)
            {
                var data = new Queue<char>(layer.ToString().ToCharArray());

                for (int y = 0; y < HEIGHT; y++)
                {
                    for (int x = 0; x < WIDTH; x++)
                    {
                        var c = data.Dequeue();
                        if (image[y, x] == TRANS)
                        {
                            image[y, x] = c;
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    if (image[y, x] == WHITE)
                    {
                        sb.Append(image[y, x]);
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString().Trim();
        }
    }
}