using System;
using System.Collections.Generic;
using PocoGenerator.ValueGenerators.RandomValueGenerators;

namespace PocoGenerator.ValueGenerators.Compound
{
    /// <summary>
    /// An IValueGenerator to generate formatted strings.
    /// </summary>
    public class FormattedStringGenerator : RandomValueGenerator, IValueGenerator<string>
    {
        string format = String.Empty;
        dynamic[] parameters;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="format">The format of the string to be generated.</param>
        /// <param name="parameters">An array of IValueGenerators whose values will be substitued into the formatted string.</param>
        public FormattedStringGenerator(string format, params dynamic[] parameters) {
            //parameters here are dynamic so that the user can pass in IValueGenerator<T>, IValueGenerator<X> ....
            //and we don't have to do any reflection-fu to call MakeOne() for arbitrary types
            this.format = format;
            this.parameters = parameters;
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <returns>The generated value.</returns>
        public string MakeOne() {
            return String.Format(format, getResultsFromParameters());
        }

        /// <summary>
        /// Gets all the valueGenerators used.
        /// </summary>
        /// <returns>The generators used to substitue values into the formatted string.</returns>
        public override IEnumerable<object> getGenerators() {
            return parameters;
        }


        private string[] getResultsFromParameters() {
            var results = new List<string>();
            foreach (var parameter in parameters)
            {
                String result = String.Empty;
                try { result = parameter.MakeOne().ToString(); }
                catch { result = parameter.ToString(); }
                results.Add(result);
            }
                
            return results.ToArray();
        }
    }
}
