using System;
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
            var machine = new IntcodeMachine(programInput, input: 1);
            Console.WriteLine("Part 1");
            machine.GetStateAfterRun();

            programInput = new List<int>(GetInput());
            machine = new IntcodeMachine(programInput, input: 5);
            Console.WriteLine("Part 2");
            machine.GetStateAfterRun();
        }

        private static List<int> GetInput()
        {
            return Utils.ParseLineToIntList(Utils.GetFilepath("day5", "day5-input"));
        }
    }
}