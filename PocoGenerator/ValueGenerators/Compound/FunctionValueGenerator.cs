using System;

namespace PocoGenerator.ValueGenerators.Compound
{
    /// <summary>
    /// An IValueGenerator which generates values from a custom funciton.
    /// </summary>
    /// <typeparam name="TParentType">The type of the parent generator.</typeparam>
    /// <typeparam name="T">The type to be generated.</typeparam>
    public class FunctionValueGenerator<TParentType, T> : ValueAwareGenerator, IValueGenerator<T>
    {
        private Func<TParentType, T> function;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="function">The custom function for creating new values.</param>
        public FunctionValueGenerator(Func<TParentType, T> function) {
            this.function = function;
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            return function.Invoke((TParentType)CurrentValue);
        }
    }
}
