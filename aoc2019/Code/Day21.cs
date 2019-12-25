using aoc2019.Misc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aoc2019.Code
{
    public class Day21 : BaseDay
    {
        string[] _part1 = new[]
        {
            "OR C T",
            "AND A T",
            "NOT T J",
            "AND D J"
        };

        string[] _part2 = new[]
        {
            "NOT C J",
            "NOT B T",
            "AND H J",
            "OR T J",
            "NOT A T",
            "OR T J",
            "AND D J",
            "RUN"
        };

        long _lastOutput;
        private readonly IEnumerable<long> _code;

        public Day21() : base(21)
        {
            _code = ReadAllText().ToIntCode();
        }

        bool RunSprintBot(IEnumerable<string> listing, string runCode)
        {
            var ans = 0L;
            IntCodeComputer comp = new IntCodeComputer(_code);
            Queue<long> input = new Queue<long>();

            comp.SignalOutput += o => ans = o;
            comp.ProvideInput += () => input.Dequeue();

            foreach (var line in listing)
            {
                input.AddLine(line);
            }
            input.AddLine(runCode);

            comp.Run();

            if (ans > 10)
            {
                lock (this)
                {
                    _lastOutput = ans;
                }

                return true;
            }
            return false;
        }

        IEnumerable<string> GenerateCode()
        {
            var s1 = new[] { "AND", "OR", "NOT" };
            var s2 = new[] { "A", "B", "C", "D", "T", "J" };
            var s3 = new[] { "T", "J" };

            foreach (var a in s1)
            {
                foreach (var b in s2)
                {
                    foreach (var c in s3)
                    {
                        yield return $"{a} {b} {c}";
                    }
                }
            }
        }

        IEnumerable<string> GenerateCode2()
        {
            var s1 = new[] { "AND", "OR", "NOT" };
            var s2 = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "T", "J" };
            var s3 = new[] { "T", "J" };

            foreach (var a in s1)
            {
                foreach (var b in s2)
                {
                    foreach (var c in s3)
                    {
                        yield return $"{a} {b} {c}";
                    }
                }
            }
        }

        public override string Part1()
        {
            _lastOutput = 0;

            RunSprintBot(_part1, "WALK");

            return _lastOutput.ToString();
        }

        public override string Part2()
        {
            _lastOutput = 0;

            RunSprintBot(_part2, "RUN");

            return _lastOutput.ToString();
        }

        public long BrutePart1()
        {
            _lastOutput = 0;

            for (int i = 4; i < 15; i++)
            {
                var bruteforce = Ext.GetCombinations(GenerateCode().ToList(), i);

                Parallel.ForEach(bruteforce, (_input, _state) =>
                {
                    var input = _input.ToList();

                    foreach (var code2 in input.GetPermutations(input.Count))
                    {
                        if (RunSprintBot(code2, "WALK"))
                        {
                            code2.ToList().ForEach(Console.WriteLine);
                            _state.Stop();
                        }
                    }
                });

                if (_lastOutput > 10)
                {
                    return _lastOutput;
                }
            }

            throw new InvalidProgramException();
        }

        public long BrutePart2()
        {
            while (true)
            {
                Parallel.For(0, int.MaxValue, (_i, _state) =>
                {
                    var r = new Random();
                    var seed = GenerateCode2().ToList();
                    var code = new List<string>();
                    while (code.Count != 4)
                    {
                        var i = r.Next(0, seed.Count);
                        code.Add(seed[i]);
                        seed.RemoveAt(i);
                    }

                    code.Add("NOT A T");
                    code.Add("OR T J");
                    code.Add("AND D J");

                    if (RunSprintBot(code, "RUN"))
                    {
                        code.ForEach(Console.WriteLine);
                        _state.Stop();
                    }
                });

                if (_lastOutput > 10)
                {
                    return _lastOutput;
                }
            }
        }
    }
}