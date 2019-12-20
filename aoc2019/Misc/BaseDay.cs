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
        bool _inputOverride;

        public int Day { get; private set; }
        public string ReadAllText() => _input;
        public string[] ReadAllLines() => _input.Split(Environment.NewLine);
        public string[] ReadAllTextSplit(string chars) => ReadAllText().Split(chars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        public IEnumerable<string[]> ReadAllLinesSplit(string chars) => ReadAllLines().Select(x => x.Split(chars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

        public BaseDay(int day)
        {
            Day = day;

            ReadInput(string.Empty);
        }

        public void OverrideInput(string input)
        {
            _input = input;
            _inputOverride = true;
        }

        public void ReadInput(string suffix)
        {
            if (!_inputOverride)
            {
                var path = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "Input",
                    $"day{Day:d2}{suffix}.txt"
                    );
                if (File.Exists(path))
                {
                    _input = File.ReadAllText(path);
                }
            }
        }

        public abstract string Part1();
        public abstract string Part2();
    }
}