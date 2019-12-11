using System;
using System.Collections.Generic;
using System.Linq;
using advent_of_code_2019.intcode;
using advent_of_code_2019.utils;

namespace advent_of_code_2019.day7
{
    public static class Day7
    {
        static List<long> programInput = null;
        public static void Run()
        {
            RunPart1();
            RunPart2();
        }

        private static void RunPart2()
        {
            long highest = 0;
            var phaseSettingOptions = GetSecondPhaseSettingOptions();

            foreach (var settingOption in phaseSettingOptions)
            {
                var machines = settingOption
                    .Select(x => new IntcodeMachine(
                        new List<long>(GetInput()), new List<long> { x })).ToList();
                long nextInput = 0;
                bool terminated = false;
                while (!terminated)
                {
                    foreach (IntcodeMachine machine in machines)
                    {
                        machine.AddInput(nextInput);
                        machine.GetStateAfterRun(out List<long> output, out bool paused);
                        terminated = !paused;
                        if (output.Count() == 0)
                            break;
                        nextInput = output.Last();
                    }
                }

                if (nextInput > highest)
                {
                    highest = nextInput;
                }
            }

            Console.WriteLine("Part 2 highest value: " + highest);
        }

        private static void RunPart1()
        {
            long highest = 0;
            var phaseSettingOptions = GetFirstPhaseSettingOptions();
            foreach (var settingOption in phaseSettingOptions)
            {
                var nextInput = 0L;
                foreach (var p in Enumerable.Range(0, 5))
                {
                    var input = new List<long> { settingOption[p], nextInput };
                    var machine = new IntcodeMachine(GetInput(), input);
                    machine.GetStateAfterRun(out List<long> output, out bool paused);
                    nextInput = output[0];
                }

                if (nextInput > highest)
                {
                    highest = nextInput;
                }
            }
            Console.WriteLine("Part 1 highest value: " + highest);
        }

        private static List<char> InsertZeroAtIndex(List<char> x, int z)
        {
            var n = new List<char>(x);
            n.Insert(z, '0');
            return n;
        }

        private static List<List<int>> GetFirstPhaseSettingOptions()
        {
            var numberOptions = new List<List<int>>();
            var charOptions = Enumerable.Range(1234, 4321 - 1234)
                .Select(x => x.ToString().ToCharArray().ToList())
                .Where(x => x.Where(z => z == '1').Count() == 1 &&
                   x.Where(z => z == '2').Count() == 1 &&
                   x.Where(z => z == '3').Count() == 1 &&
                   x.Where(z => z == '4').Count() == 1);
            foreach (var option in charOptions)
            {
                foreach (var position in Enumerable.Range(0, 5))
                {
                    numberOptions.Add(InsertZeroAtIndex(option, position)
                        .ToList().Select(x => int.Parse(x.ToString())).ToList());
                }
            }
            // Day 4 called, they want their code back
            return numberOptions;

        }
        private static List<List<int>> GetSecondPhaseSettingOptions()
        {
            var numberOptions = new List<List<int>>();
            var charOptions = Enumerable.Range(50000, 99999 - 50000)
                .Select(x => x.ToString().ToCharArray().ToList())
                .Where(x => x.Where(z => z == '5').Count() == 1 &&
                   x.Where(z => z == '6').Count() == 1 &&
                   x.Where(z => z == '7').Count() == 1 &&
                   x.Where(z => z == '8').Count() == 1 &&
                   x.Where(z => z == '9').Count() == 1);
            foreach (var option in charOptions)
            {
                numberOptions.Add(option.Select(x => int.Parse(x.ToString())).ToList());
            }
            return numberOptions;
        }

        private static List<long> GetInput()
        {
            if (programInput == null)
            {
                programInput = Utils
                    .ParseLineToIntList(Utils.GetFilepath("day7", "day7-input"))
                    .Select(x => (long)x).ToList();
            }
            return programInput;
        }
    }
}