using aoc2019.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2019.Code
{
    public class Day07 : BaseDay
    {
        public Day07() : base(7)
        {

        }

        IntCodeComputer GetAmp() => new IntCodeComputer(ReadAllText().ToIntCode());

        public override string Part1()
        {
            var max = long.MinValue;

            foreach (var seq in "01234".Permutate())
            {
                long output = 0;

                for (int i = 0; i < 5; i++)
                {
                    var c = GetAmp();
                    c.AddInput(int.Parse(seq[i].ToString()));
                    c.AddInput(output);
                    c.SignalOutput += _output => output = _output;
                    c.Run();
                }

                if (output > max)
                {
                    max = output;
                }
            }

            return max.ToString();
        }

        public override string Part2()
        {
            var max = long.MinValue;

            foreach (var seq in "56789".Permutate())
            {
                var phase = new Queue<int>(seq.ToCharArray().Select(x => int.Parse(x.ToString())));
                var output = RunPart2(phase);

                if (output > max)
                {
                    max = output;
                }
            }

            return max.ToString();
        }

        long RunPart2(Queue<int> seq)
        {
            long lastOutput = 0;

            var ampA = GetAmp();
            var ampB = GetAmp();
            var ampC = GetAmp();
            var ampD = GetAmp();
            var ampE = GetAmp();

            ampA.AddInput(seq.Dequeue());
            ampA.AddInput(0);

            ampA.SignalOutput += _signal => AddInputs(ampB, GetAmpParams(_signal));
            ampB.SignalOutput += _signal => AddInputs(ampC, GetAmpParams(_signal));
            ampC.SignalOutput += _signal => AddInputs(ampD, GetAmpParams(_signal));
            ampD.SignalOutput += _signal => AddInputs(ampE, GetAmpParams(_signal));

            ampE.SignalOutput += _signal =>
            {
                AddInputs(ampA, GetAmpParams(_signal));
                lastOutput = _signal;
            };

            var amp = new List<Task>
            {
               new Task( ampA.Run),
               new Task( ampB.Run),
               new Task( ampC.Run),
               new Task( ampD.Run),
               new Task( ampE.Run),
            };
            amp.ForEach(x => x.Start());
            Task.WhenAll(amp).ConfigureAwait(false).GetAwaiter().GetResult();

            return lastOutput;

            long[] GetAmpParams(long signal) => seq.Count > 0 ? new[] { seq.Dequeue(), signal } : new[] { signal };
            void AddInputs(IntCodeComputer amp, long[] queue) => queue.ToList().ForEach(x => amp.AddInput(x));
        }
    }
}