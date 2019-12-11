using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using advent_of_code_2019.intcode;

namespace advent_of_code_2019.day2
{
    public static class Day2
    {
        private static List<long> cleanNumbers = null;

        public static void Run()
        {
            try
            {
                var nouns = Enumerable.Range(0, 100);
                var verbs = Enumerable.Range(0, 100);
                foreach (int noun in nouns)
                {
                    foreach (int verb in verbs)
                    {
                        var machine = new IntcodeMachine(new List<long>(GetNumbers()));
                        machine.Modify(1, noun);
                        machine.Modify(2, verb);
                        var stateAfterRun = machine.GetStateAfterRun(out List<long> output, out bool paused);
                        if (stateAfterRun[0] == 19690720)
                        {
                            throw new Exception($"Found it! Noun: {noun}, Verb: {verb}");
                        }
                    }
                }
                Console.WriteLine("Finished writing program.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static List<long> GetNumbers()
        {
            if (cleanNumbers == null)
            {
                var filePath = Path.Combine(
                    new string[] { Directory.GetCurrentDirectory(), "day2", "day2-input" }
                );
                var content = File.ReadAllText(filePath);
                cleanNumbers = content.Split(",").ToList().Select(item => long.Parse(item)).ToList();
            }
            return cleanNumbers;
        }
    }
}