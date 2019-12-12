using System;
using System.Collections.Generic;
using System.Linq;
using advent_of_code_2019.intcode;
using advent_of_code_2019.utils;

namespace advent_of_code_2019.day11
{
    public static class Day11
    {
        private static List<long> programInput;

        private enum Direction
        {
            Up = 0, Right = 1, Down = 2, Left = 3
        }

        private static (int, int) GetNewCoords(
            (int, int) oldCoords,
            Direction direction,
            long rotation,
            out Direction newDirection)
        {
            bool turnRight = rotation == 1;
            switch (direction)
            {
                case Direction.Up:
                    newDirection = turnRight ? Direction.Right : Direction.Left;
                    return (oldCoords.Item1 + (turnRight ? 1 : -1), oldCoords.Item2);
                case Direction.Right:
                    newDirection = turnRight ? Direction.Down : Direction.Up;
                    return (oldCoords.Item1, oldCoords.Item2 + (turnRight ? -1 : 1));
                case Direction.Down:
                    newDirection = turnRight ? Direction.Left : Direction.Right;
                    return (oldCoords.Item1 + (turnRight ? -1 : 1), oldCoords.Item2);
                case Direction.Left:
                    newDirection = turnRight ? Direction.Up : Direction.Down;
                    return (oldCoords.Item1, oldCoords.Item2 + (turnRight ? 1 : -1));
                default:
                    throw new Exception("something went wrong");
            }
        }

        public static void Run()
        {
            var robot = new IntcodeMachine(
                new List<long>(GetInput()),
                new List<long> { 0 });

            var direction = Direction.Up;
            var whiteCoords = new List<(int, int)>();
            var anyCoords = new List<(int, int)>();
            var currentCoords = (0, 0);
            bool terminated = false;
            bool currentlyOnBlackPaint = false;

            while (!terminated)
            {
                robot.FeedJustOneTypeOfInputRepeatedly(currentlyOnBlackPaint ? 0 : 1);
                robot.GetStateAfterRun(out List<long> colorOfPaint, out bool paused);
                if (colorOfPaint.Count() == 0)
                {
                    terminated = true;
                    break;
                }
                if (colorOfPaint.First() == 1 && !whiteCoords.Contains(currentCoords))
                    whiteCoords.Add(currentCoords);
                else if (whiteCoords.Contains(currentCoords))
                    whiteCoords.Remove(currentCoords);

                robot.GetStateAfterRun(out List<long> rotation, out paused);
                anyCoords.Add(currentCoords);
                currentCoords = GetNewCoords(currentCoords, direction, rotation.First(), out Direction newDirection);
                direction = newDirection;
                currentlyOnBlackPaint = !whiteCoords.Contains(currentCoords);
            }

            Console.WriteLine("This many painted once: " + anyCoords.Distinct().Count());

            foreach (var Y in Enumerable.Range(-50, 100))
            {
                var acc = "";
                foreach (var X in Enumerable.Range(-60, 100))
                {
                    acc += whiteCoords.Contains((X, Y)) ? "0" : " ";
                }
                Console.WriteLine(acc);
            }
        }

        private static List<long> GetInput()
        {
            if (programInput == null)
            {
                programInput = Utils
                    .ParseLineToLongList(Utils.GetFilepath("day11", "day11-input"));
                programInput.AddRange(Enumerable.Repeat(0, 1000000)
                    .Select(x => (long)x).ToList());
            }
            return programInput;
        }
    }
}