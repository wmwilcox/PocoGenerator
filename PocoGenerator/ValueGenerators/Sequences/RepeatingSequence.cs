using System.Collections.Generic;
using System.Linq;

namespace PocoGenerator.ValueGenerators.Sequences
{
    /// <summary>
    /// An IValueGenerator for creating values from a repeating sequence.
    /// </summary>
    /// <typeparam name="T">The type to be generated.</typeparam>
    public class RepeatingSequence<T> : IValueGenerator<T>
    {
        private T[] values;
        int curIndex = 0;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="values">The values which define the repeating sequence.</param>
        public RepeatingSequence(IEnumerable<T> values) {
            this.values = values.ToArray();
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            T value = values[curIndex];
            curIndex++;
            if (curIndex > values.Length - 1)
                curIndex = 0;
            return value;
        }
    }
}
