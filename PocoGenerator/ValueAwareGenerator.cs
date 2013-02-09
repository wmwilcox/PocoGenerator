
namespace PocoGenerator
{
    /// <summary>
    /// A class used by PocoGenerator to pass along the current instance of the value being generated
    /// so that the member generators can inspect it when creating their own values.
    /// </summary>
    public class ValueAwareGenerator : RandomValueGenerator
    {
        /// <summary>
        /// The current value.
        /// </summary>
        public object CurrentValue { get; set; }

        internal static void InformGeneratorOfCurrentValue(object valueGenerator, object value) {
            var valAwareGenerator = valueGenerator as ValueAwareGenerator;
            if (valAwareGenerator != null)
                valAwareGenerator.CurrentValue = value;
        }

    }
}
