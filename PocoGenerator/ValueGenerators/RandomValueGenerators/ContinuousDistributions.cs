using System;

namespace PocoGenerator.ValueGenerators.RandomValueGenerators
{
    /// <summary>
    /// An IValueGenerator for doubles which selects from a uniform distribution.
    /// </summary>
    public class FromUniformGenerator : RandomValueGenerator, IValueGenerator<double>
    {
        private double min;
        private double diff;

        /// <summary>
        /// Creates a new instances.
        /// </summary>
        /// <param name="min">The minimum value of the range to select from.</param>
        /// <param name="max">The maximum value of the range to select from.</param>
        public FromUniformGenerator(double min, double max) {
            this.min = min;
            diff = (max - min);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public double MakeOne() {
            return min + (RandomSource.NextDouble() * diff);
        }
    }

    /// <summary>
    /// An IValueGenerator for doubles which selects from a pareto distribution.
    /// </summary>
    /// <see cref="!:http://en.wikipedia.org/wiki/Pareto_distribution"/>
    public class FromParetoGenerator : RandomValueGenerator, IValueGenerator<double>
    {
        private double scale;
        private double shape;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="scale">The scale parameter for the pareto distribution.</param>
        /// <param name="shape">The shape parameter for the pareto distribution.</param>
        public FromParetoGenerator(double scale, double shape) {
            FromParetoGenerator.checkParameters(scale, shape);
            this.scale = scale;
            this.shape = shape;  
        }

        /// <summary>
        /// Creates a value from a given pareto distribution.
        /// </summary>
        /// <param name="rnd">The random source.</param>
        /// <param name="scale">The scale parameter for the pareto distribution.</param>
        /// <param name="shape">The shape parameter for the pareto distribution.</param>
        /// <returns>A value selected from the given pareto distribution.</returns>
        public static Double FromPareto(Random rnd, double scale, double shape) {
            FromParetoGenerator.checkParameters(scale, shape);
            return scale * Math.Pow(rnd.NextDouble(), -1.0 / shape);
        }

        private static void checkParameters(double scale, double shape){
            if (scale <= 0 || Double.IsNaN(scale))
                throw new ArgumentOutOfRangeException("scale", "scale must be a number greater than zero.");
            if (shape <= 0 || Double.IsNaN(shape))
                throw new ArgumentOutOfRangeException("shape", "shape must be a number greater than zero.");
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public double MakeOne() {
            return FromParetoGenerator.FromPareto(RandomSource, scale, shape);
        }
    }

    /// <summary>
    /// An IValueGenerator for doubles which selects from a normal distribution.
    /// </summary>
    /// <see cref="!:http://en.wikipedia.org/wiki/Normal_distribution"/>
    public class FromNormalGenerator : RandomValueGenerator, IValueGenerator<double>
    {
        private double mean;
        private double stddev;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        public FromNormalGenerator(double mean, double stddev) {
            this.mean = mean;
            this.stddev = stddev;
        }

        /// <summary>
        /// Creates a value from a given normal distribution.
        /// </summary>
        /// <param name="rnd">The random source.</param>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        /// <returns>A value selected from the given normal distribution.</returns>
        public static double RandNormal(Random rnd, double mean, double stddev) {
            var v1 = (2.0 * rnd.NextDouble()) - 1.0;
            var v2 = (2.0 * rnd.NextDouble()) - 1.0;
            var r = (v1 * v1) + (v2 * v2);
            while (r >= 1.0 || r == 0.0) {
                v1 = (2.0 * rnd.NextDouble()) - 1.0;
                v2 = (2.0 * rnd.NextDouble()) - 1.0;
                r = (v1 * v1) + (v2 * v2);
            }
            var f = Math.Sqrt(-2.0 * Math.Log(r) / r);
            return mean + (stddev*(v1 * f));
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public double MakeOne() {
            return FromNormalGenerator.RandNormal(RandomSource, mean, stddev);
        }
    }
}
