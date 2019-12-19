using aoc2019.Misc;

namespace aoc2019.Code
{
    public class Day13 : BaseDay
    {
        public Day13() : base(13)
        {

        }

        public override string Part1()
        {
            var c = new IntCodeComputer(ReadAllText().ToIntCode());

            var skip = 0;
            var cnt = 0;

            c.SignalOutput += _output =>
            {
                if (skip == 2)
                {
                    if (_output == 2)
                    {
                        cnt++;
                    }
                    skip = -1;
                }
                skip++;
            };

            c.Run();

            return cnt.ToString();
        }

        public override string Part2()
        {
            long px = 0, bx = 0;
            long score = 0;
            long[] buf = new long[3];
            int i = 0;
            var c = new IntCodeComputer(ReadAllText().ToIntCode());

            c.SignalOutput += _output =>
            {
                buf[i++] = _output;

                if (i == 3)
                {
                    if (buf[0] == -1 && buf[1] == 0)
                    {
                        score = buf[2];
                    }
                    else
                    {
                        switch (buf[2])
                        {
                            case 4:
                                bx = buf[0];
                                break;
                            case 3:
                                px = buf[0];
                                break;
                        }

                    }

                    i = 0;
                }
            };

            c.ProvideInput += () =>
            {
                if (bx > px)
                {
                    return 1;
                }
                else if (bx < px)
                {
                    return -1;
                }
                return 0;
            };

            c[0] = 2;
            c.Run();

            return score.ToString();
        }
    }
}