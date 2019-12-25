using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace aoc2019.Code
{
    public class Day25 : BaseDay
    {
        public Day25() : base(25)
        {

        }

        readonly string[] _safeItems =
        {
            //"photons",
            //"giant electromagnet",
            //"infinite loop",
            //"molten lava",
            //"escape pod",

            "easter egg",
            "astrolabe",
            "space law space brochure",
            "manifold",
            "fuel cell",
            "hologram",
            "weather machine",
            "antenna"
        };

        enum State
        {
            GetItems,
            AtSecurity
        }

        public override string Part1()
        {
            var s = State.GetItems;
            var r = new Random();
            var c = new IntCodeComputer(ReadAllText().ToIntCode());
            var input = new Queue<long>();
            var sb = new StringBuilder();
            var doors = new List<string>();
            var items = new HashSet<string>();
            var inv = new HashSet<string>();
            var text = new List<string>();
            var bruteforce = new Queue<List<string>>();

            c.SignalOutput += _o =>
            {
                if (_o == 10)
                {
                    text.Add(sb.ToString());
                    sb.Clear();

                    if ((s == State.AtSecurity || s == State.GetItems) && text.Last() == "Command?")
                    {
                        var nowdoor = false;
                        var nowitems = false;
                        foreach (var line in text)
                        {
                            if (nowitems && line.StartsWith("-"))
                            {
                                var item = line.Substring(2);
                                if (_safeItems.Contains(item))
                                {
                                    items.Add(item);
                                    input.AddLine($"take {items.Last()}");
                                }
                            }
                            else if (nowdoor && line.StartsWith("-"))
                            {
                                doors.Add(line.Substring(2));
                            }
                            if (line == "Doors here lead:")
                            {
                                nowdoor = true;
                                doors.Clear();
                            }
                            else if (line == "Items here:")
                            {
                                nowitems = true;
                            }
                        }

                        if (text.Any(x => x.Contains("verify your identity.")) && _safeItems.All(x => items.Contains(x)))
                        {
                            s = State.AtSecurity;

                            var all = new List<string[]>();
                            for (int i = 1; i < items.Count; i++)
                            {
                                var inv = items.ToList().GetCombinations(i).ToArray().Select(p => p.ToArray());
                                foreach (var set in inv)
                                {
                                    var temp = new List<string>();

                                    // drop all items
                                    foreach (var olditem in items)
                                    {
                                        temp.Add($"drop {olditem}");
                                    }

                                    // pickup only needed to enter
                                    foreach (var ii in set)
                                    {
                                        temp.Add($"take {ii}");
                                    }

                                    // go south
                                    temp.Add("south");

                                    // store for later                                    
                                    bruteforce.Enqueue(temp);
                                }
                            }
                        }

                        text.Clear();
                    }
                }
                else
                {
                    sb.Append((char) _o);
                }
            };

            c.ProvideInput += () =>
            {
                if (!input.Any())
                {
                    if (s == State.GetItems)
                    {
                        input.AddLine(doors[r.Next(0, doors.Count)]);
                    }
                    else if (s == State.AtSecurity)
                    {
                        bruteforce.Dequeue().ForEach(x => input.AddLine(x));
                    }
                }

                return input.Dequeue();
            };

            c.Run();

            return string.Join(Environment.NewLine, text);
        }

        public override string Part2() => Regex.Match(Part1(), @"\d+").Value;
    }
}