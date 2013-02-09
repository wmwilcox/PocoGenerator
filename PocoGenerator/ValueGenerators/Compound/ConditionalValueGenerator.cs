using PocoGenerator.ValueGenerators.Literals;
using System;
using System.Collections.Generic;

namespace PocoGenerator.ValueGenerators.Compound
{
    /// <summary>
    /// An IValueGenerator whose generated values can be customized based on the current value of the parent object.
    /// </summary>
    /// <typeparam name="TParentType">The type of the parent object.</typeparam>
    /// <typeparam name="T">The type of the object to be generated.</typeparam>
    public class ConditionalValueGenerator<TParentType, T> : ValueAwareGenerator, IValueGenerator<T>
    {
        private List<KeyValuePair<Predicate<TParentType>, IValueGenerator<T>>> conditionsAndValues;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ConditionalValueGenerator() {
            conditionsAndValues = new List<KeyValuePair<Predicate<TParentType>, IValueGenerator<T>>>();
        }

        /// <summary>
        /// Adds a condition.
        /// </summary>
        /// <param name="condition">A predicate representing the condition.</param>
        /// <param name="value">The generator which will be used if the condition is met.</param>
        /// <returns>this</returns>
        public ConditionalValueGenerator<TParentType, T> AddCondition(Predicate<TParentType> condition, IValueGenerator<T> value) {
            conditionsAndValues.Add
                (new KeyValuePair<Predicate<TParentType>, IValueGenerator<T>>
                    (condition, value));
            return this;
        }

        /// <summary>
        /// Adds a condition.
        /// </summary>
        /// <param name="condition">A predicate representing the condition.</param>
        /// <param name="value">The value which will be used if the condition is met.</param>
        /// <returns>this</returns>
        public ConditionalValueGenerator<TParentType, T> AddCondition(Predicate<TParentType> condition, T value) {
            conditionsAndValues.Add
                (new KeyValuePair<Predicate<TParentType>, IValueGenerator<T>>
                    (condition, new LiteralValueGenerator<T>(value)));
            return this;
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            foreach (var conditionValuePair in conditionsAndValues)
            {
                if (conditionValuePair.Key.Invoke((TParentType)CurrentValue))
                    return conditionValuePair.Value.MakeOne();
            }
            return default(T);
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The generators associated with conditions.</returns>
        public override IEnumerable<object> getGenerators() {
            var generators = new List<object>();
            foreach (var pair in conditionsAndValues)
                generators.Add(pair.Value);
            return generators;
        }
    }
}
