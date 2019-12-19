using aoc2019.Misc;
using System;
using System.Linq;

namespace aoc2019.Code
{
    public class Day01 : BaseDay
    {
        static int Calc(int input) => (int) Math.Floor(input / 3.0) - 2;

        public Day01() : base(1)
        {

        }

        public override string Part1()
        {
            return ReadAllLines().Select(x => Calc(int.Parse(x))).Sum().ToString();
        }

        public override string Part2()
        {
            int total = 0;
            foreach (var module in ReadAllLines().Select(int.Parse))
            {
                int current = module;
                while (true)
                {
                    var fuel = Calc(current);
                    if (fuel <= 0)
                    {
                        break;
                    }
                    total += fuel;
                    current = fuel;
                }
            }
            return total.ToString();
        }
    }
}