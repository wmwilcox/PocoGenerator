
namespace PocoGenerator.ValueGenerators.Literals
{
    /// <summary>
    /// A value generator used to create literal values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LiteralValueGenerator<T> : IValueGenerator<T>
    {
        private T value;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="value">The literal value to be used.</param>
        public LiteralValueGenerator(T value) {
            this.value = value;
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            return value;
        }
    }
}
