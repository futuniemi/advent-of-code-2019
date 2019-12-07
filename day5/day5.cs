using advent_of_code_2019.utils;
using advent_of_code_2019.intcode;
using System.Collections.Generic;

namespace advent_of_code_2019.day5
{
    public static class Day5
    {
        public static void Run()
        {
            var machine = new IntcodeMachine(GetInput(), input: 1);
            var state = machine.GetStateAfterRun();
        }

        private static List<int> GetInput()
        {
            return Utils.ParseLineToIntList(Utils.GetFilepath("day5", "day5-input"));
        }
    }
}