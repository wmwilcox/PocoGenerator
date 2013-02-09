using System;
using System.Collections.Generic;
using System.Linq;

namespace PocoGenerator.ValueGenerators.RandomValueGenerators
{
    /// <summary>
    /// An IValueGenerator which randomly selects elements from a weighted set.
    /// </summary>
    /// <typeparam name="T">The type to generate.</typeparam>
    public class WeightedSelector<T>: RandomValueGenerator, IValueGenerator<T>
    {
        private Double[] weights;
        private T[] values;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="source">The set of values and their associated weights. Used to create the distribution to select values from.</param>
        public WeightedSelector(IEnumerable<KeyValuePair<T, int>> source) {
            var values = new List<T>();
            var weights = new List<double>();
            double totalWeight = 0d;
            double totalSize = source.Sum(x => x.Value);
            foreach(KeyValuePair<T,int> item in source){
                totalWeight += ((double)item.Value / totalSize);
                weights.Add(totalWeight);
                values.Add(item.Key);
            }

            this.weights = weights.ToArray();
            this.values = values.ToArray();
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            double selection = RandomSource.NextDouble();
            int index = Array.BinarySearch(weights, selection);
            if (index < 0)
                index = ~index;
            return values[index];
        }
    }
}
