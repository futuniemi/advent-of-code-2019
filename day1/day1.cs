using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2019.day1 {
    public static class Day1 {
        public static void Run () {
            Console.WriteLine (AggregateFuel (GetFuelAmounts ()));
        }

        public static int AggregateFuel (IEnumerable<int> fuelRequirements) {
            return fuelRequirements.Aggregate (0, (acc, fuel) => acc + Calculate (fuel));
        }

        public static int Calculate (int fuel) {
            return (fuel / 3) - 2;
        }

        public static IEnumerable<int> GetFuelAmounts () {

            var filePath = Path.Combine (
                new string[] { Directory.GetCurrentDirectory (), "day1", "day1-input" }
            );

            List<int> numbers = new List<int> ();
            string line;
            using (var reader = new StreamReader (filePath)) {
                while ((line = reader.ReadLine ()) != null) {
                    numbers.Add (int.Parse (line));
                }
            }
            return numbers;
        }
    }
}