using System;
using System.Collections.Generic;
using System.IO;

namespace PocoGenerator.Common
{
    /// <summary>
    /// Contains helper methods for processing data in files.
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// Creates a Func that will convert a string into a weighted value. Assumes value is first, and weight is seperated by a tab character.
        /// </summary>
        public static Func<string, KeyValuePair<string, int>> WeightedValueTSVLineParser = new Func<string, KeyValuePair<string, int>>((line) =>
        {
            var pcs = line.Split('\t');
            return new KeyValuePair<string, int>(pcs[0], Int32.Parse(pcs[1]));

        });

        /// <summary>
        /// Method to read custom values from a text file.
        /// </summary>
        /// <typeparam name="T">The type to be read from the file.</typeparam>
        /// <param name="reader">The reader used to read the file.</param>
        /// <param name="valueSelector">This function is used to convert each line into some T.</param>
        /// <returns>A list of converted values, each value read from a line from the TextReader.</returns>
        public static List<T> ReadValuesFromTextFile<T>(TextReader reader, Func<string, T> valueSelector) {
            var result = new List<T>();

            string line = String.Empty;

            while ((line = reader.ReadLine()) != null)
                result.Add(valueSelector(line));

            return result;
        }

        /// <summary>
        /// This method creates a weighted set by reading the lines of a text file.
        /// </summary>
        /// <typeparam name="T">The type to be read from the file.</typeparam>
        /// <param name="reader">The reader used to read the file.</param>
        /// <param name="valueSelector">This function is used to convert each line into some T.</param>
        /// <returns>A dictionary indexed by the values, pointing to their associated weights.</returns>
        public static Dictionary<T, int> ReadWeightedValuesFromTextFile<T>(TextReader reader, Func<string, KeyValuePair<T, int>> valueSelector) {
            var result = new Dictionary<T, int>();

            string line = String.Empty;

            while ((line = reader.ReadLine()) != null)
            {
                var value = valueSelector(line);
                result.Add(value.Key, value.Value);
            }

            return result;
        }
    }
}
