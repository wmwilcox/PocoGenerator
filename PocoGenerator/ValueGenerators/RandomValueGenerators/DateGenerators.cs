using System;
using System.Collections.Generic;

namespace PocoGenerator.ValueGenerators.RandomValueGenerators
{
    /// <summary>
    /// An IValueGenerator for generating DateTimes from a uniform distribution.
    /// </summary>
    public class DateFromUniformGenerator : RandomValueGenerator, IValueGenerator<DateTime>
    {
        private long min;
        private long max;
        private LongFromUniformGenerator selector;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="minDate">The minimum value of the range to select from.</param>
        /// <param name="maxDate">The maximum value of the range to select from.</param>
        public DateFromUniformGenerator(DateTime minDate, DateTime maxDate) {
            min = minDate.Ticks;
            max = maxDate.Ticks;
            selector = new LongFromUniformGenerator(min, max);
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public DateTime MakeOne() {
            return new DateTime(selector.MakeOne());
        }


        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The uniform generator used to select values.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { selector };
        }
    }

    /// <summary>
    /// An IValueGenerator for generating DateTimes from a normal distribution.
    /// </summary>
    public class DateFromNormalGenerator : RandomValueGenerator, IValueGenerator<DateTime>
    {
        private long mean;
        private long stddev;
        private LongFromNormalGenerator selector;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="meanDate">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        public DateFromNormalGenerator(DateTime meanDate, TimeSpan stddev) {
            this.mean = meanDate.Ticks;
            this.stddev = stddev.Ticks;
            selector = new LongFromNormalGenerator(mean, this.stddev);
        }


        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public DateTime MakeOne() {
            return new DateTime(selector.MakeOne());
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The normal generator used to select values.</returns>
        public override IEnumerable<object> getGenerators() {
            return new List<object>() { selector };
        }
    }
}
