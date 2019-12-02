using System;
using System.Collections.Generic;
using advent_of_code_2019.day1;
using advent_of_code_2019.day2;

namespace advent_of_code_2019
{
    class Program
    {
        static Dictionary<int, Action> DaySolutions =
            new Dictionary<int, Action>() {
                { 1, Day1.Run } ,
                { 2, Day2.Run }
            };

        static void Main(string[] args)
        {
            Console.WriteLine("\nEnter day number:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int inputNumber) &&
                DaySolutions.TryGetValue(inputNumber, out Action RunFunction))
            {
                RunFunction();
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Number, please. Now start over.");
            }
        }
    }
}