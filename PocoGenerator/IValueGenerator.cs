
namespace PocoGenerator
{
    //out keyword makes would have made this interface covariant (see: http://msdn.microsoft.com/en-us/library/dd233059.aspx)
    // this worked fine, but this is not supported for type values (see above). In light of this,
    // it becomes messy to deal with. Each IValueGenerator<T> implementation must be implemented as
    // IValueGenerator<object> if you were ever to use a type value. This, of course, is a big sacrifice
    // in terms of IDE support.
    
    /// <summary>
    /// Interface used by PocoGenerator for assigning generators to properties of POCOs.
    /// </summary>
    /// <typeparam name="T">The type of value the generator returns.</typeparam>
    public interface IValueGenerator<T>
    {
        /*
         * A little history on the attempts used in this interface.
         *
         * First, the out keyword was used here to make the interface covariant (see: http://msdn.microsoft.com/en-us/library/dd233059.aspx).
         * This allowed things such as IValueGenerator<object> x = new ValueGenerator<String>(), etc.
         * However, this does not work with value types (see above) - meaning that the DSL in POCOGenerator would lose support
         * of intellisense when defining IValueGenerators for value type properties such as int, double, boolean, etc.
         * 
         * Next, these were pulled pulled from the internal collection of generators as dynamic, and the MakeOne() method
         * called without compile-time type checking. However, this resulted in some very strange behaviour which caused 
         * some tests to fail. See PocoGeneratorTest.TestThatSettingRandomSourceDoesNotCauseAnInfinteLoop
         * 
         */


        /// <summary>
        /// Generates a value.
        /// </summary>
        /// <returns>A new instance of T.</returns>
        T MakeOne();
    }
}
