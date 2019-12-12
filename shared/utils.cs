using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2019.utils
{

    public static class Utils
    {
        public static IEnumerable<T> ParseInput<T>(string filePath, Func<string, T> Parse)
        {
            List<T> items = new List<T>();
            string line;
            using (var reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    items.Add(Parse(line));
                }
            }
            return items;
        }

        public static List<int> ParseLineToIntList(string filePath)
        {
            var content = File.ReadAllText(filePath);
            return content.Split(",").ToList().Select(item => int.Parse(item)).ToList();
        }

        public static List<long> ParseLineToLongList(string filePath)
        {
            var content = File.ReadAllText(filePath);
            return content.Split(",").ToList().Select(item => long.Parse(item)).ToList();
        }

        public static string GetFilepath(string directory, string filename)
        {
            return Path.Combine(
                new string[] { Directory.GetCurrentDirectory(), directory, filename }
            );
        }
    }
}