using aoc2019.Misc;
using System;

namespace aoc2019.Code
{
    public class Day02 : BaseDay
    {
        public Day02() : base(2)
        {

        }

        string Run(int a, int b)
        {
            var c = new IntCodeComputer(ReadAllText().ToIntCode());
            c[1] = a;
            c[2] = b;
            c.Run();

            return c[0].ToString();
        }

        public override string Part1() => Run(12, 2);

        public override string Part2()
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    if (Run(noun, verb) == "19690720")
                    {
                        return (100 * noun + verb).ToString();
                    }
                }
            }
            throw new InvalidProgramException();
        }
    }
}