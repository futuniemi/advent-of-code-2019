using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2019.utils {

    public static class Utils {
        public static IEnumerable<T> ParseInput<T> (string filePath, Func<string, T> Parse) {
            List<T> items = new List<T> ();
            string line;
            using (var reader = new StreamReader (filePath)) {
                while ((line = reader.ReadLine ()) != null) {
                    items.Add (Parse (line));
                }
            }
            return items;
        }

        public static string GetFilepath (string directory, string filename) {
            return Path.Combine (
                new string[] { Directory.GetCurrentDirectory (), directory, filename }
            );
        }
    }
}