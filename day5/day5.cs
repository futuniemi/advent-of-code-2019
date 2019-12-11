using System;
using System.Linq;
using System.Collections.Generic;
using advent_of_code_2019.utils;
using advent_of_code_2019.intcode;

namespace advent_of_code_2019.day5
{
    public static class Day5
    {
        public static void Run()
        {
            var programInput = new List<long>(GetInput());
            var machine = new IntcodeMachine(programInput, input: new List<long> { 1 });
            machine.GetStateAfterRun(out List<long> output1, out bool paused1);
            Console.WriteLine("Part 1: " + output1.Last());

            programInput = new List<long>(GetInput());
            machine = new IntcodeMachine(programInput, input: new List<long> { 5 });
            machine.GetStateAfterRun(out List<long> output2, out bool paused2);
            Console.WriteLine("Part 2: " + output2.Last());
        }

        private static List<long> GetInput()
        {
            return Utils
                .ParseLineToIntList(Utils.GetFilepath("day5", "day5-input"))
                .Select(x => (long)x).ToList();
        }
    }
}