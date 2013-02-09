using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocoGenerator
{
    /// <summary>
    /// Used by PocoGenerator to propogate the RandomSource among all its children value generators.
    /// </summary>
    public class RandomValueGenerator
    {
        /// <summary>
        /// The source where random values are selected.
        /// </summary>
        protected Random randomSource;

        /// <summary>
        /// Gets or Sets the RandomSource used as the random source for selecting values for random generators.
        /// When the source is set, the value will be propogated down the tree of generator objects through 
        /// the path defined by the implementation of getGenerators.
        /// </summary>
        public Random RandomSource {
            get {
                if (randomSource == null)
                    return new Random();
                else
                    return randomSource;
            }
            set {
                if (!Object.ReferenceEquals(this.RandomSource, value))
                {
                    this.randomSource = value;
                    afterRandomSourceSet();
                }
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RandomValueGenerator() { }

        /// <summary>
        /// Method which defines all dependent value generators used by the current instance.
        /// This is used for propogating changes to the RandomSource down the dependency tree.
        /// </summary>
        /// <returns>All depenedent value generators.</returns>
        public virtual IEnumerable<object> getGenerators() { return new List<object>(); }

        private void afterRandomSourceSet() {
            foreach (object d in getGenerators()){
                RandomValueGenerator.InformGeneratorOfRandomSource(d, this.RandomSource);
            }
        }

        internal static void InformGeneratorOfRandomSource(object valueGenerator, Random randomSource) {
            var rndGenerator = valueGenerator as RandomValueGenerator;
            if (rndGenerator != null)
                rndGenerator.RandomSource = randomSource;
        }
    }
}
