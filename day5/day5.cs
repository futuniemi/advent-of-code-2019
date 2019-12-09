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
            var programInput = new List<int>(GetInput());
            var machine = new IntcodeMachine(programInput, input: new List<int> { 1 });
            machine.GetStateAfterRun(out List<int> output1, out bool paused1);
            Console.WriteLine("Part 1" + output1.Last());

            programInput = new List<int>(GetInput());
            machine = new IntcodeMachine(programInput, input: new List<int> { 5 });
            machine.GetStateAfterRun(out List<int> output2, out bool paused2);
            Console.WriteLine("Part 2: " + output2.Last());
        }

        private static List<int> GetInput()
        {
            return Utils.ParseLineToIntList(Utils.GetFilepath("day5", "day5-input"));
        }
    }
}