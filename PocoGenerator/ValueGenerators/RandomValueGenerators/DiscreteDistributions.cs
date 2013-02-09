using System;
using System.Collections.Generic;

namespace PocoGenerator.ValueGenerators.RandomValueGenerators
{
    /// <summary>
    /// An IValueGenerator to create integers from a uniform distribution.
    /// </summary>
    public class IntFromUniformGenerator : RandomValueGenerator, IValueGenerator<int>
    {
        private int min;
        private int max;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="min">The minimum of the range to select from.</param>
        /// <param name="max">The maximum of the range to select from.</param>
        public IntFromUniformGenerator(int min, int max) {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public int MakeOne() {
            return RandomSource.Next(min, max);
        }
    }

    /// <summary>
    /// An IValueGenerator to create longs from a uniform distribution.
    /// </summary>
    public class LongFromUniformGenerator : RandomValueGenerator, IValueGenerator<long>
    {
        private long minimum;
        private double diff;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="min">The minimum of the range to select from.</param>
        /// <param name="max">The maximum of the range to select from.</param>
        public LongFromUniformGenerator(long min, long max) {
            this.minimum = min;
            diff = (max - min);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public long MakeOne() {
            return minimum + (long)Math.Round(RandomSource.NextDouble() * diff, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// An IValueGenerator for generating integers from a normal distribution.
    /// </summary>
    public class IntFromNormalGenerator : RandomValueGenerator, IValueGenerator<int>
    {
        private FromNormalGenerator normalGenerator;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        public IntFromNormalGenerator(int mean, int stddev) {
            normalGenerator = new FromNormalGenerator((double)mean, (double)stddev);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public int MakeOne() {
            return (int)(normalGenerator.MakeOne());
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The normalGenerator used to select values.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { normalGenerator };
        }
    }

    /// <summary>
    /// An IValueGenerator for generating longs from a normal distribution.
    /// </summary>
    public class LongFromNormalGenerator : RandomValueGenerator, IValueGenerator<long>
    {
        private FromNormalGenerator normalGenerator;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        public LongFromNormalGenerator(long mean, long stddev) {
            normalGenerator = new FromNormalGenerator((double)mean, (double)stddev);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public long MakeOne() {
            return (long)Math.Round(normalGenerator.MakeOne());
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The normalGenerator used to select values.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { normalGenerator };
        }
    }

    /// <summary>
    /// An IValueGenerator for generating integers from a pareto distribution.
    /// </summary>
    public class IntFromParetoGenerator : RandomValueGenerator, IValueGenerator<int>
    {
        private FromParetoGenerator paretoGenerator;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="scale">The scale parameter of the pareto distribution.</param>
        /// <param name="shape">The shape parameter of the pareto distribution.</param>
        public IntFromParetoGenerator(double scale, double shape) {
            paretoGenerator = new FromParetoGenerator(scale, shape);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public int MakeOne() {
            return (int)Math.Round(paretoGenerator.MakeOne());
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The paretoGenerator used to select values.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { paretoGenerator };
        }
    }

    /// <summary>
    /// An IValueGenerator for generating longs from a pareto distribution.
    /// </summary>
    public class LongFromParetoGenerator : RandomValueGenerator, IValueGenerator<long>
    {
        private FromParetoGenerator paretoGenerator;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="scale">The scale parameter of the pareto distribution.</param>
        /// <param name="shape">The shape parameter of the pareto distribution.</param>
        public LongFromParetoGenerator(double scale, double shape) {
            paretoGenerator = new FromParetoGenerator(scale, shape);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public long MakeOne() {
            return (long)Math.Round(paretoGenerator.MakeOne());
        }


        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The paretoGenerator used to select values.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { paretoGenerator };
        }
    }
}
