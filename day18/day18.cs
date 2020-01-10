using System;
using System.Collections.Generic;
using System.Linq;
using advent_of_code_2019.utils;

namespace advent_of_code_2019.day18
{
    using Map = Dictionary<(int, int), char>;
    public enum Dir { U, R, D, L }
    public static class Day18
    {
        public static void Run()
        {
            var map = new TunnelMap();
            var entranceCoords = map.GetCoordsAt('@');
            Console.WriteLine("EntranceCoords: " + entranceCoords);
            var test = map.MapKeysAndDoorsInSight(entranceCoords);
            // Look by traversing: Where is next key
            // where equivalent door is in sight (record equivalent door)
            // If multiple, select shortest path
            // Actually Traverse to selected key (record steps)
            // When at key, set door as unlocked
            // Repeat
        }
    }

    class TunnelMap
    {
        private Map map;
        public TunnelMap()
        {
            this.map = new Map();
            var y = 0;
            // Utils.ParseInput<char[]>(Utils.GetFilepath("day18", "day18-input"), r => r.ToCharArray())
            var example1 = new String[] { "#########", "#b.A.@.a#", "#########" };
            var z = example1.Select(x => x.ToCharArray());
            z.ToList().ForEach(ca => { var x = 0; ca.ToList().ForEach(c => { map.Add((x, y), c); x++; }); y++; });
        }

        public (int, int) GetCoordsAt(char x) => this.map.Where(z => z.Value == x).First().Key;

        public static bool isKey(char k) => char.ToUpper(k) != k;
        public static bool isDoor(char d) => char.ToLower(d) != d;
        public static bool isOpenPassage(char o) => o == '.';
        public static bool isStoneWall(char s) => s == '#';

        public (int, int) MapKeysAndDoorsInSight((int, int) startCoord)
        {
            var allPaths = new List<((int, int), int)>();
            var multiChoicePlaces = new List<(int, int)>(); // Should keep record of steps taken
            var currentCoords = startCoord;
            (int, int)? prevCoords = null;
            do
            {
                while (true)
                {
                    var count = 0;
                    var choices = GetChoices(currentCoords, prevCoords);
                    if (choices.Count == 0)
                    {
                        break;
                    }
                    else if (choices.Count > 1)
                    {
                        multiChoicePlaces.AddRange(choices.GetRange(1, choices.Count - 1));
                    }
                    prevCoords = currentCoords;
                    currentCoords = choices.First();
                    count++;
                }

            } while (multiChoicePlaces.Count() != 0);

            // Must traverse all possible directions
            // And get all available keys and doors
            // What's the stop condition?

            // Here I am
            // Options? If yes, then pick first, add others as coords & dir identifier to Options List
            // If options list is empty, return all paths
            return (0, 0);
        }

        public List<(int, int)> GetChoices((int, int) location, (int, int)? whereICameFrom)
        {
            var options = new List<(int, int)>();
            foreach (var op in Operations)
            {
                var nextPlace = op(location);
                if (map.TryGetValue(nextPlace, out char x) && isOpenPassage(x) && nextPlace != whereICameFrom)
                    options.Add(nextPlace);
            }
            return options;
        }

        public List<Func<(int, int), (int, int)>> Operations = new List<Func<(int, int), (int, int)>> { GetAbove, GetRight, GetBelow, GetLeft };
        public static (int, int) GetAbove((int, int) l) => (l.Item1, l.Item2 - 1);
        public static (int, int) GetRight((int, int) l) => (l.Item1 + 1, l.Item2);
        public static (int, int) GetBelow((int, int) l) => (l.Item1, l.Item2 + 1);
        public static (int, int) GetLeft((int, int) l) => (l.Item1 - 1, l.Item2);
    }
}