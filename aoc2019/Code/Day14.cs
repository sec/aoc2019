using aoc2019.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Code
{
    public class Day14 : BaseDay
    {
        const long CARGO = 1_000_000_000_000L;

        IList<Recipe> _input;

        public Day14() : base(14)
        {

        }

        class Recipe
        {
            public Item Output;
            public List<Item> Input;

            public override string ToString() => $"{string.Join(" ", Input)} => {Output}";
        }

        class Item
        {
            public string Name;
            public int Count;

            public Item(string name, int count)
            {
                Name = name;
                Count = count;
            }

            public override string ToString() => $"{Count} {Name}";

            public static Item Get(string data)
            {
                var s = data.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                return new Item(s[1], int.Parse(s[0]));
            }
        }

        IEnumerable<Recipe> ReadInput()
        {
            if (_input == null)
            {
                _input = new List<Recipe>();

                var input = ReadAllLines();
                foreach (var line in input)
                {
                    var d = line.Split("=>");
                    var left = d[0].Split(", ");
                    var right = d[1];

                    _input.Add(new Recipe()
                    {
                        Input = left.Select(Item.Get).ToList(),
                        Output = Item.Get(right)
                    });
                }
            }
            return _input;
        }

        public override string Part1() => OreRequired("FUEL", 1, new DefaultDict<string, long>()).ToString();

        public override string Part2()
        {
            var left = 1L;
            var right = CARGO;

            while (left < right)
            {
                var index = (left + right) / 2;
                var used = OreRequired("FUEL", index, new DefaultDict<string, long>());
                if (used < CARGO)
                {
                    // we can produce more
                    left = index + 1;
                }
                else
                {
                    // too much
                    right = index - 1;
                }
            }

            return left.ToString();
        }

        long OreRequired(string what, long count, DefaultDict<string, long> stock)
        {
            if (what == "ORE")
            {
                return count;
            }

            if (stock[what] > 0)
            {
                var howMuchWeCanUse = Math.Min(count, stock[what]);
                stock[what] -= howMuchWeCanUse;
                count -= howMuchWeCanUse;
            }

            if (count == 0)
            {
                return 0;
            }

            var item = ReadInput().Single(x => x.Output.Name == what);
            var react = 1L;
            if (count > item.Output.Count)
            {
                react = (long) Math.Ceiling((double) count / item.Output.Count);
            }
            stock[what] += react * item.Output.Count - count;

            var ore = 0L;
            foreach (var n in item.Input)
            {
                ore += OreRequired(n.Name, react * n.Count, stock);
            }

            return ore;
        }
    }
}