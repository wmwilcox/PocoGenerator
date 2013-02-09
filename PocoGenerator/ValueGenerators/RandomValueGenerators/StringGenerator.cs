using PocoGenerator.ValueGenerators.Literals;
using System.Collections.Generic;
using System.Text;

namespace PocoGenerator.ValueGenerators.RandomValueGenerators
{
    /// <summary>
    /// An IValueGenerator for generating random strings.
    /// </summary>
    public class StringGenerator : RandomValueGenerator, IValueGenerator<string>
    {
        private IValueGenerator<string> wordSelector;
        private IValueGenerator<int> sizeSelector;

        /// <summary>
        /// Creates a new instances.
        /// </summary>
        /// <param name="size">The length of the strings to be generated.</param>
        /// <param name="wordSelector">An IValueGenerator to generate segments of the random string.</param>
        public StringGenerator(int size, IValueGenerator<string> wordSelector) {
            sizeSelector = new LiteralValueGenerator<int>(size);
            this.wordSelector = wordSelector;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="sizeSelector">An IValueGenerator used to determine the length of the string to generate.</param>
        /// <param name="wordSelector">An IValueGenerator to generate segments of the random string.</param>
        public StringGenerator(IValueGenerator<int> sizeSelector, IValueGenerator<string> wordSelector) {
            this.sizeSelector = sizeSelector;
            this.wordSelector = wordSelector;
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The generators used to select values and size.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { wordSelector, sizeSelector };
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public string MakeOne() {
            int curSize = 0;
            var size = sizeSelector.MakeOne();

            var builder = new StringBuilder();
            while (curSize < size)
            {
                var curWord = wordSelector.MakeOne();
                builder.Append(curWord).Append(" ");
                curSize += (curWord.Length + 1);
            }

            if (size > 0)
                return builder.ToString().Substring(0, size);
            else
                return "";
        }
    }
}
