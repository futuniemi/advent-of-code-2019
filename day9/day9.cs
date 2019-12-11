using System;
using System.Collections.Generic;
using System.Linq;
using advent_of_code_2019.intcode;
using advent_of_code_2019.utils;

namespace advent_of_code_2019.day9
{
    public static class Day9
    {
        static List<long> programInput = null;
        public static void Run()
        {
            var input = new List<long>(GetInput());
            var range = Enumerable.Repeat(0, 1000000).Select(x => (long)x).ToList();
            input.AddRange(range); // 😬
            var machine = new IntcodeMachine(input, new List<long> { 2 });
            machine.GetStateAfterRun(out List<long> output, out bool paused);
            output.ForEach(x => Console.WriteLine(x));
        }

        private static List<long> GetInput()
        {
            if (programInput == null)
            {
                programInput = Utils.ParseLineToIntList(
                    Utils.GetFilepath("day9", "day9-input")).Select(x => (long)x).ToList();
            }
            return programInput;
        }
    }
}