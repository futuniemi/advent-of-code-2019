using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2019.day3
{
    public static class Day3
    {
        public static void Run()
        {
            GetWireInstructions(out List<string> wire1, out List<string> wire2);
            var wireMap1 = CreateCoordinateMap(wire1);
            var wireMap2 = CreateCoordinateMap(wire2);

            var intersections = wireMap1.Intersect(wireMap2);
            var sortedMap = intersections
                .Select(i => new { coord = i, sum = Math.Abs(i.Item1) + Math.Abs(i.Item2) })
                .OrderBy(x => x.sum);
            Console.Write("\n\n Closest intersection: " + sortedMap.ElementAt(1));

            var firstIntersection = intersections.ElementAt(1);
            var totalSteps = wireMap1.IndexOf(firstIntersection) +
                wireMap2.IndexOf(firstIntersection);
            Console.Write($"\n\n Steps to first intersection: {totalSteps.ToString()}\n\n");
        }

        private static List<(int, int)> CreateCoordinateMap(List<string> wireInstructions)
        {
            var pointer = (0, 0);
            var coordinates = new List<(int, int)>();
            coordinates.Add(pointer);
            foreach (string instruction in wireInstructions)
            {
                var command = instruction.First();
                var amount = int.Parse(instruction.Substring(1));
                List<(int, int)> elapsedCoordinates;
                switch (command)
                {
                    case 'U':
                        elapsedCoordinates = Process(pointer, amount, GoUp);
                        break;
                    case 'D':
                        elapsedCoordinates = Process(pointer, amount, GoDown);
                        break;
                    case 'R':
                        elapsedCoordinates = Process(pointer, amount, GoRight);
                        break;
                    case 'L':
                        elapsedCoordinates = Process(pointer, amount, GoLeft);
                        break;
                    default:
                        throw new Exception("Ecountered an unknown command");
                }
                coordinates.AddRange(elapsedCoordinates);
                pointer = elapsedCoordinates.Last();
            }
            coordinates.Add(pointer);
            return coordinates;
        }

        private static (int, int) GoUp((int, int) startPoint, int amount)
            => (startPoint.Item1, startPoint.Item2 + amount);
        private static (int, int) GoDown((int, int) startPoint, int amount)
            => (startPoint.Item1, startPoint.Item2 - amount);
        private static (int, int) GoRight((int, int) startPoint, int amount)
            => (startPoint.Item1 + amount, startPoint.Item2);
        private static (int, int) GoLeft((int, int) startPoint, int amount)
            => (startPoint.Item1 - amount, startPoint.Item2);

        private static List<(int, int)> Process(
            (int, int) initialPointer,
            int amount,
            Func<(int, int), int, (int, int)> Go)
            => Enumerable.Range(1, amount).Select(x => Go(initialPointer, x)).ToList();

        private static void GetWireInstructions(out List<string> wire1, out List<string> wire2)
        {
            var filePath = Path.Combine(
                new string[] { Directory.GetCurrentDirectory(), "day3", "day3-input" }
            );
            using (var reader = new StreamReader(filePath))
            {
                wire1 = reader.ReadLine().Split(",").ToList();
                wire2 = reader.ReadLine().Split(",").ToList();
            }
        }
    }
}