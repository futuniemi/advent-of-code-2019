using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2019.day4
{
    public static class Day4
    {
        public static void Run()
        {
            Console.WriteLine("Answer to Part 1: " + CountPart1());
            Console.WriteLine("Answer to Part 2: " + CountPart2());
        }

        private static int CountPart1() => GetBase()
            .Where(x => Enumerable.Range(0, 5).Any(z => x[z] == x[z + 1]))
            .Count();

        private static int CountPart2() => GetBase()
            .Where(x => Enumerable.Range(0, 5).Any(z => x[z] == x[z + 1]
                        && x.Where(n => n == x[z]).Count() == 2))
            .Count();

        private static IEnumerable<char[]> GetBase() =>
            Enumerable.Range(134792, 675810 - 134792)
            .Select(x => x.ToString().ToCharArray())
            .Where(x => x.SequenceEqual(x.OrderBy(y => y)));
    }
}