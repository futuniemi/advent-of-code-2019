using System;
using System.Collections.Generic;
using System.Linq;
using advent_of_code_2019.utils;

namespace advent_of_code_2019.day6 {
    public static class Day6 {
        private static IEnumerable < (string, string) > dependencies;
        private static int counter;

        public static void Run () {
            GetDependencies ();
            var uniqueNodes = GetUniqueNodes ();
            uniqueNodes.ForEach (TravelHome);
            Console.WriteLine ("Part 1: " + counter);

            var youRoute = GetHomeRoute ("YOU");
            var sanRoute = GetHomeRoute ("SAN");
            var diffRoute = youRoute.Except (sanRoute).Union (sanRoute.Except (youRoute));
            Console.WriteLine ("Part 2: " + diffRoute.Count ());
        }

        private static List<string> GetUniqueNodes () =>
            dependencies.Aggregate (new List<string> (), (acc, x) => {
                if (!acc.Contains (x.Item2)) {
                    acc.Add (x.Item2);
                };
                return acc;
            });

        private static void TravelHome (string value) {
            var node = dependencies.Where (x => x.Item2 == value).First ();
            counter++;
            if (node.Item1 != "COM") {
                TravelHome (node.Item1);
            }
        }

        private static List<string> GetHomeRoute (string value) {
            var route = new List<string> ();
            var next = value;
            while (next != "COM") {
                var node = dependencies.Where (x => x.Item2 == next).First ();
                route.Add (node.Item1);
                next = node.Item1;
            }
            return route;
        }

        private static void GetDependencies () {
            dependencies = Utils.ParseInput < (string, string) > (
                Utils.GetFilepath ("day6", "day6-input"),
                input => (input.Substring (0, 3), input.Substring (4)));
        }
    }
}