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

        public static void Run()
        {
            program = GetNumbers();
            try
            {
                IterateProgram();
            }
            catch
            {
                Console.WriteLine(String.Join(", ", program));
            }
        }

        private static void IterateProgram()
        {
            var maxLength = program.Count();
            while (pointer < maxLength)
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
                        Terminate();
                        break;
                    default:
                        throw new Exception("Command not 1, 2 or 99");
                }
                pointer += 4;
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

        private static void Terminate()
        {
            throw new Exception("Program terminated");
        }

        private static List<int> GetNumbers()
        {
            var filePath = Path.Combine(
                new string[] { Directory.GetCurrentDirectory(), "day2", "day2-input" }
            );
            var content = File.ReadAllText(filePath);
            return content.Split(",").ToList().Select(item => int.Parse(item)).ToList();
        }
    }
}