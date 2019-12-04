using System;
using System.Linq;

namespace advent_of_code_2019.day4
{
    public static class Day4
    {
        public static void Run()
        {
            var possiblePasswords = Enumerable.Range(134792, 675810 - 134792)
                .Select(x => x.ToString().ToCharArray())
                .Where(x => x.SequenceEqual(x.OrderBy(y => y)))
                .Where(x => Enumerable.Range(0, 5).Any(z => x[z] == x[z + 1]));
            Console.WriteLine(possiblePasswords.Count());
        }
    }
}