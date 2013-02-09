using PocoGenerator.Common;
using PocoGenerator.ValueGenerators.Literals;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PocoGenerator
{
    /// <summary>
    /// A class used to generate POCOs with defined customizable data.
    /// </summary>
    /// <typeparam name="T">The type of the POCO to be generated. This type must have a default public constructor.</typeparam>
    public class PocoGenerator <T> : RandomValueGenerator, IValueGenerator<T> where T : new()
    {
        /// <summary>
        /// This list contains the names of all the properties/members which this generator has a definition,
        /// in the order that the definition was given.
        /// </summary>
        protected List<String> generatedPropertiesInOrder = new List<string>();
        /// <summary>
        /// This list contains all generatedCollectionDefintion(of T) which are definied for this generator,
        /// in the order that they were defined.
        /// </summary>
        protected List<object> generatedCollectionDefinitions = new List<object>();
        /// <summary>
        /// This dictionary contains all IValueGenerator(of T), indexed by the property names they were defined for.
        /// </summary>
        protected Dictionary<String, object> generators = new Dictionary<String, object>();
        /// <summary>
        /// This dictionary contains all MemberInfos for property that have an IValueGenerator(of T) defined, indexed by the property/member name.
        /// </summary>
        protected Dictionary<String, MemberInfo> memberInfos = new Dictionary<String, MemberInfo>();

        /// <summary>
        /// The optional function which the generator would use to create the initial instance of T when generating.
        /// </summary>
        protected Func<T> initializer = null;
        /// <summary>
        /// The optional function which the generator would execute after it has completed all other generation steps.
        /// </summary>
        protected Action<T> finalizer = null;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public PocoGenerator() { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="randomSource">The random source for generating values.</param>
        public PocoGenerator(Random randomSource) {
            RandomSource = randomSource;
        }

        /// <summary>
        /// Represents the current state of the value that is being generated. Used for propogation of the value for ValueAwareGenerators.
        /// </summary>
        public T CurrrentValue { get; set; }

        internal void AddPropertyDefinition(String propertyName, object valueGenerator) {
            RandomValueGenerator.InformGeneratorOfRandomSource(valueGenerator, this.RandomSource);
            generators.Add(propertyName, valueGenerator);
            generatedPropertiesInOrder.Add(propertyName);
        }

        internal void AddGeneratedCollectionDefinition(object collectionDefinition) {
            RandomValueGenerator.InformGeneratorOfRandomSource(collectionDefinition, this.RandomSource);
            this.generatedCollectionDefinitions.Add(collectionDefinition);
        }

        /// <summary>
        /// Clears the instances current defintions, and copies all definitions for an existing PocoGenerator.
        /// </summary>
        /// <param name="parent">The parent generator to copy from.</param>
        /// <returns>this</returns>
        public PocoGenerator<T> CloneFrom(PocoGenerator<T> parent) {

            this.generatedPropertiesInOrder.Clear();
            foreach (var x in parent.generatedPropertiesInOrder)
                this.generatedPropertiesInOrder.Add(x);
            this.generatedCollectionDefinitions.Clear();
            foreach (var x in parent.generatedCollectionDefinitions)
                this.generatedCollectionDefinitions.Add(x);

            this.generators.Clear();
            foreach (var key in parent.generators.Keys)
                this.generators.Add(key, parent.generators[key]);
            this.memberInfos.Clear();
            foreach (var key in parent.memberInfos.Keys)
                this.memberInfos.Add(key, parent.memberInfos[key]);

            this.initializer = parent.initializer;
            this.finalizer = parent.finalizer;
            this.RandomSource = parent.RandomSource;
            return this;
        }

        /// <summary>
        /// Define a funciton which will return the initial instance of the POCO that will be used each time a new value is generated.
        /// </summary>
        /// <param name="initializer">The function to run to initialize the POCO.</param>
        /// <returns>this</returns>
        public PocoGenerator<T> WithInitializer(Func<T> initializer) {
            this.initializer = initializer;
            return this;
        }

        /// <summary>
        /// Define a function which will be called when the generator has completed.
        /// </summary>
        /// <param name="finalizer">The function to be called.</param>
        /// <returns>this</returns>
        public PocoGenerator<T> WithFinalizer(Action<T> finalizer) {
            this.finalizer = finalizer;
            return this;
        }

        /// <summary>
        /// Selects a property from the POCO which can then be assigned a value generator.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of the property selected.</typeparam>
        /// <param name="selector">The expression which will define what property is selected.</param>
        /// <returns>A new instance of PocoPropertyDefinition</returns>
        public PocoPropertyDefinition<TPropertyType, T> For<TPropertyType>(Expression<Func<T, TPropertyType>> selector) {
            MemberInfo member = ReflectionUtils.GetPropertyFromExpression(selector);
            memberInfos.Add(member.Name, member);
            return new PocoPropertyDefinition<TPropertyType, T>(this, member.Name);
        }

        /// <summary>
        /// Defines a GeneratedCollectionDefinition to be used.
        /// Basically, x number a certain value are created w/ an IValueGenerator, and these values 
        /// are made available to a custom function through the returned GeneratedCollectionDefinition.
        /// </summary>
        /// <typeparam name="TCollectionValueType">The type to be generated.</typeparam>
        /// <param name="sizeGenerator">The generator which determines the number of values to generate.</param>
        /// <param name="valueGenerator">The generator which will create the values.</param>
        /// <returns>A new instance of GeneratedCollectionDefinition</returns>
        public GeneratedCollectionDefinition<TCollectionValueType, T> WithValues<TCollectionValueType>
            (IValueGenerator<int> sizeGenerator, IValueGenerator<TCollectionValueType> valueGenerator) {
                return new GeneratedCollectionDefinition<TCollectionValueType, T>(this, sizeGenerator, valueGenerator);
        }

        /// <summary>
        /// Defines a GeneratedCollectionDefinition to be used.
        /// Basically, x number a certain value are created w/ an IValueGenerator, and these values 
        /// are made available to a custom function through the returned GeneratedCollectionDefinition.
        /// </summary>
        /// <typeparam name="TCollectionValueType">The type to be generated.</typeparam>
        /// <param name="size">The number of values to generate.</param>
        /// <param name="valueGenerator">The generator which will create the values.</param>
        /// <returns>A new instance of GeneratedCollectionDefinition</returns>
        public GeneratedCollectionDefinition<TCollectionValueType, T> WithGeneratedCollection<TCollectionValueType>
            (int size, IValueGenerator<TCollectionValueType> valueGenerator) {
                return new GeneratedCollectionDefinition<TCollectionValueType, T>(this, new LiteralValueGenerator<int>(size), valueGenerator);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public T MakeOne() {
            T value = getInitialValue();
            CurrrentValue = value;
            MemberInfo member = null;

            foreach (var collectionDefinition in generatedCollectionDefinitions)
            {
                var defType = collectionDefinition.GetType();
                defType.InvokeMember("Apply", BindingFlags.InvokeMethod, null, collectionDefinition, new object[]{value});
                //collectionDefinition.Apply(value);
            }

            foreach (String propertyName in generatedPropertiesInOrder)
            {
                member = memberInfos[propertyName];
                var generator = generators[propertyName];
                ValueAwareGenerator.InformGeneratorOfCurrentValue(generator, value);

                var generatorType = generator.GetType();
                var valueFromInvocation = generatorType.InvokeMember("MakeOne", BindingFlags.InvokeMethod, null, generator, null);
                ReflectionUtils.SetMemberValue(member, value, valueFromInvocation);
            }

            return finalize(value);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public List<T> Make(int n) {
            List<T> list = new List<T>();
            for (int i = 0; i < n; i++)
                list.Add(MakeOne());
            return list;
        }

        /// <summary>
        /// Executes the finalize function if defined.
        /// </summary>
        /// <param name="value">The current value of the POCO being generated.</param>
        /// <returns>The value after finalize has ran.</returns>
        protected T finalize(T value) {
            if (finalizer != null)
                finalizer(value);
            return value;
        }

        /// <summary>
        /// Returns the initial value of the POCO.
        /// </summary>
        /// <returns>Either the default value or the one returned by the initializer (if defined).</returns>
        protected T getInitialValue() {
            if (initializer == null)
                return Activator.CreateInstance<T>();
            else
                return initializer();
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The generators used create all values in the POCO.</returns>
        public override IEnumerable<object> getGenerators() {
            var generators = new List<object>();
            foreach (object definition in this.generatedCollectionDefinitions)
                generators.Add(definition);
            foreach (object definition in this.generators.Values)
                generators.Add(definition);

            return generators;
        }

    }
}
