using System.Collections;
using System.Collections.Generic;

namespace PocoGenerator
{
    /// <summary>
    /// Conveniance wrapper around Dictionary T,int
    /// Created to simply collection initialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeightedSet<T> : IEnumerable<KeyValuePair<T,int>>
    {
        /// <summary>
        /// The underlying weighted set.
        /// </summary>
        public Dictionary<T, int> WeightedValues { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public WeightedSet() {
            WeightedValues = new Dictionary<T, int>();
        }

        /// <summary>
        /// Adds a key value pair to the weighted set.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The weight assigned to this key.</param>
        public void Add(T key, int value) {
            WeightedValues.Add(key, value);
        }

        /// <summary>
        /// Returns the enumerator of the underlying dictionary.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<T, int>> GetEnumerator() {
            return WeightedValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return WeightedValues.GetEnumerator();
        }
    }
}
