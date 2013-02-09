using System.Collections.Generic;
using System.Linq;

namespace PocoGenerator.ValueGenerators.RandomValueGenerators
{
    /// <summary>
    /// An IValueGenerator which randomly selects values from a set.
    /// </summary>
    /// <typeparam name="T">The type to be generated.</typeparam>
    public class Selector<T> : RandomValueGenerator, IValueGenerator<T> 
    {
        /// <summary>
        /// Defines the set of values to select from.
        /// </summary>
        protected T[] possibleValues;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="sourceCollection">The collection to select from.</param>
        public Selector(IEnumerable<T> sourceCollection) {
            possibleValues = sourceCollection.ToArray<T>();
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            int index = RandomSource.Next(0, possibleValues.Length - 1);
            return possibleValues[index];
        }
    }
}
