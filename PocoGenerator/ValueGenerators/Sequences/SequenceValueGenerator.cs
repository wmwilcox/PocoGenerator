
namespace PocoGenerator.ValueGenerators.Sequences
{
    internal class SequenceValueGenerator<T> : IValueGenerator<T>
    {
        private dynamic incrementBy;
        private dynamic curValue;

        public SequenceValueGenerator(dynamic start, dynamic incrementBy) {
            this.curValue = start;
            this.incrementBy = incrementBy;
        }

        public T MakeOne() {
            dynamic value = curValue;
            curValue = (curValue += incrementBy);
            return value;
        }
    }
}
