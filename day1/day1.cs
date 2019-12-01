using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2019.day1 {
    public static class Day1 {
        public static void Run () {
            Console.WriteLine (AggregateFuel (GetFuelAmounts (), CalculateRealFuel));
        }

        private static int AggregateFuel (IEnumerable<int> fuelRequirements, Func<int, int> Calculate) {
            return fuelRequirements.Aggregate (0, (acc, fuel) => acc + Calculate (fuel));
        }

        private static int CalculateNaiveFuel (int fuel) {
            return (fuel / 3) - 2;
        }

        private static int CalculateRealFuel (int fuel) {
            int extraFuel = fuel;
            int aggregator = 0;
            do {
                extraFuel = CalculateNaiveFuel (extraFuel);
                aggregator += extraFuel > 0 ? extraFuel : 0;
            } while (extraFuel > 0);
            return aggregator;
        }

        private static IEnumerable<int> GetFuelAmounts () {

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