using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace aoc2019.Misc
{
    public abstract class BaseDay : IDay
    {
        string _input;

        public int Day { get; private set; }
        public string ReadAllText() => _input;
        public string[] ReadAllLines() => _input.Split(Environment.NewLine);
        public string[] ReadAllTextSplit(string chars) => ReadAllText().Split(chars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        public IEnumerable<string[]> ReadAllLinesSplit(string chars) => ReadAllLines().Select(x => x.Split(chars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

        public BaseDay(int day)
        {
            Day = day;

            var path = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Input",
                $"day{Day:d2}.txt"
                );
            if (File.Exists(path))
            {
                _input = File.ReadAllText(path);
            }
        }

        public void OverrideInput(string input) => _input = input;

        public abstract string Part1();
        public abstract string Part2();
    }
}