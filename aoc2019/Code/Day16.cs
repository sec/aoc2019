using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019.Code
{
    public class Day16 : BaseDay
    {
        public Day16() : base(16)
        {

        }

        public override string Part1() => Process(ReadAllText());

        string Process(string input)
        {
            var data = input.Select(x => int.Parse(x.ToString()));
            var one = new List<int>(data);
            var two = new List<int>(data);

            for (int c = 0; c < 100; c++)
            {
                Phase(one, two);

                var swap = one;
                one = two;
                two = swap;
            }

            return string.Join(string.Empty, one.Take(8));
        }

        public override string Part2()
        {
            var sb = new StringBuilder();
            foreach (var i in Enumerable.Range(0, 10000))
            {
                sb.Append(ReadAllText());
            }

            var offset = int.Parse(ReadAllText().Substring(0, 7));
            var input = sb.ToString().Substring(offset).Select(x => int.Parse(x.ToString())).ToList();

            foreach (var c in Enumerable.Range(0, 100))
            {
                for (int i = input.Count - 2; i >= 0; i--)
                {
                    input[i] = (input[i] + input[i + 1]) % 10;
                }
            }

            return string.Join(string.Empty, input.Take(8));
        }

        void Phase(List<int> input, List<int> output)
        {
            for (var i = 0; i < input.Count; i++)
            {
                var index = 0;
                var p = GetPattern(i + 1).Take(input.Count).ToList();
                output[i] = Math.Abs(input.Sum(x => x * p[index++]) % 10);
            };
        }

        IEnumerable<int> GetPattern(int level)
        {
            var firstSkip = false;
            var pattern = new int[] { 0, 1, 0, -1 };
            var index = 0;

            while (true)
            {
                for (var count = 0; count < level; count++)
                {
                    if (!firstSkip)
                    {
                        firstSkip = true;
                        continue;
                    }
                    yield return pattern[index % 4];
                }
                index++;
            }
        }
    }
}