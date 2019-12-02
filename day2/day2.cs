using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2019.day2
{
    public static class Day2
    {
        private static int pointer = 0;
        private static List<int> program;
        private static List<int> cleanNumbers = null;

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
                        pointer = 0;
                        program = new List<int>(GetNumbers());
                        program[1] = noun;
                        program[2] = verb;
                        IterateProgram();
                        if (program[0] == 19690720)
                        {
                            throw new Exception($"Found it! Noun: {noun}, Verb: {verb}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void IterateProgram()
        {
            var stop = false;
            var maxLength = program.Count();
            while (pointer < maxLength && !stop)
            {
                var opCode = program[pointer];
                var param1 = program[pointer + 1];
                var param2 = program[pointer + 2];
                var param3 = program[pointer + 3];
                switch (program[pointer])
                {
                    case 1:
                        Add(param1, param2, param3);
                        break;
                    case 2:
                        Multiply(param1, param2, param3);
                        break;
                    case 99:
                        stop = true;
                        break;
                    default:
                        throw new Exception("Command not 1, 2 or 99");
                }
                pointer += 4;
                if (stop)
                    break;
            }
        }

        private static void Add(int param1, int param2, int param3)
        {
            program[param3] = program[param1] + program[param2];
        }

        private static void Multiply(int param1, int param2, int param3)
        {
            program[param3] = program[param1] * program[param2];
        }

        private static List<int> GetNumbers()
        {
            if (cleanNumbers == null)
            {
                var filePath = Path.Combine(
                    new string[] { Directory.GetCurrentDirectory(), "day2", "day2-input" }
                );
                var content = File.ReadAllText(filePath);
                cleanNumbers = content.Split(",").ToList().Select(item => int.Parse(item)).ToList();
            }
            return cleanNumbers;
        }
    }
}