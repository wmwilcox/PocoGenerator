using System;
using System.Collections.Generic;

namespace PocoGenerator
{
    /// <summary>
    /// This class is used by PocoGenerator(of T).WithValuse method. It allows for custom handling of a list of generated objects.
    /// </summary>
    /// <typeparam name="T">The type to be generated. This is a type for some member of TParent.</typeparam>
    /// <typeparam name="TParent">The parent type</typeparam>
    public class GeneratedCollectionDefinition<T, TParent> : RandomValueGenerator where TParent : new()
    {
        /// <summary>
        /// The parent generator. Used as return value for methods to support DSL feature.
        /// </summary>
        protected PocoGenerator<TParent> parentGenerator;
        /// <summary>
        /// The action to be performed. Evaluated against each generated value.
        /// </summary>
        protected Action<TParent, T> action;
        /// <summary>
        /// The generator used to generate values to be consumed by the custom action.
        /// </summary>
        protected IValueGenerator<T> valueGenerator;
        /// <summary>
        /// The generator which decides how many values of type T to create.
        /// </summary>
        protected IValueGenerator<int> sizeGenerator;

        /// <summary>
        /// Creates a new instance of GeneratedCollectionDefintion.
        /// </summary>
        /// <param name="parent">The parent generator.</param>
        /// <param name="sizeGenerator">Used to decide how many values to generate.</param>
        /// <param name="valueGenerator">Used to decide how to generate the values.</param>
        public GeneratedCollectionDefinition(PocoGenerator<TParent> parent, IValueGenerator<int> sizeGenerator, IValueGenerator<T> valueGenerator) {
            this.parentGenerator = parent;
            this.valueGenerator = valueGenerator;
            this.sizeGenerator = sizeGenerator;
        }

        /// <summary>
        /// Used to define the action that will be used.
        /// </summary>
        /// <param name="action">The custom action to be used.</param>
        /// <returns>The parent PocoGenerator</returns>
        public PocoGenerator<TParent> Do(Action<TParent, T> action) {
            this.action = action;
            parentGenerator.AddGeneratedCollectionDefinition(this);
            return parentGenerator;
        }

        /// <summary>
        /// This method executes the action for all the generated values.
        /// </summary>
        /// <param name="currentValue">The current value. When called by the parent generator, it is the current state of the value being generated.</param>
        public void Apply(TParent currentValue) {
            foreach (var value in generateList())
                action.Invoke(currentValue, value);
        }

        /// <summary>
        /// Provides information about all generators used.
        /// </summary>
        /// <returns>All the generators used.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { valueGenerator, sizeGenerator };
        }

        private List<T> generateList() {
            var result = new List<T>();
            for (int i = 0; i < sizeGenerator.MakeOne(); i++)
                result.Add(valueGenerator.MakeOne());
            return result;
        }
    }
}
