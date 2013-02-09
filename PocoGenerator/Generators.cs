using PocoGenerator.Common;
using PocoGenerator.ValueGenerators.Compound;
using PocoGenerator.ValueGenerators.RandomValueGenerators;
using PocoGenerator.ValueGenerators.Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PocoGenerator
{
    /// <summary>
    /// A factory class containing static conveniance methods for instatiating IValueGenerators
    /// </summary>
    public class Generators
    {

        #region Sequnce Generators

        /// <summary>
        /// Creates a generator which will create a sequence of integers starting with 0, incrementing by 1.
        /// </summary>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<int> IntSequence() {
            return new SequenceValueGenerator<int>(0, 1);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of integers starting w/ the specified start value, incremented by 1.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<int> IntSequence(int startValue) {
            return new SequenceValueGenerator<int>(startValue, 1);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of integers starting w/ the specified start value, incremented by a specified value.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <param name="incrementBy">The value to increment by.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<int> IntSequence(int startValue, int incrementBy) {
            return new SequenceValueGenerator<int>(startValue, incrementBy);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of unsigned integers starting with 0, incrementing by 1.
        /// </summary>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<uint> UIntSequence() {
            return new SequenceValueGenerator<uint>((uint)0, (uint)1);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of unsigned integers starting w/ the specified start value, incremented by 1.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<uint> UIntSequence(uint startValue) {
            return new SequenceValueGenerator<uint>(startValue, (uint)1);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of unsigned integers starting w/ the specified start value, incremented by a specified value.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <param name="incrementBy">The value to increment by.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<uint> UIntSequence(uint startValue, uint incrementBy) {
            return new SequenceValueGenerator<uint>(startValue, incrementBy);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of longs starting with 0, incrementing by 1.
        /// </summary>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<long> LongSequence() {
            return new SequenceValueGenerator<long>(0L, 1L);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of longs starting w/ the specified start value, incremented by 1.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<long> LongSequence(long startValue) {
            return new SequenceValueGenerator<long>(startValue, 1L);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of longs starting w/ the specified start value, incremented by a specified value.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <param name="incrementBy">The value to increment by.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<long> LongSequence(long startValue, long incrementBy) {
            return new SequenceValueGenerator<long>(startValue, incrementBy);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of unsinged longs starting with 0, incrementing by 1.
        /// </summary>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<ulong> ULongSequence() {
            return new SequenceValueGenerator<ulong>((ulong)0L, (ulong)1L);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of unsigned longs starting w/ the specified start value, incremented by 1.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<ulong> ULongSequence(ulong startValue) {
            return new SequenceValueGenerator<ulong>(startValue, (ulong)1L);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of unsigned longs starting w/ the specified start value, incremented by a specified value.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <param name="incrementBy">The value to increment by.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<ulong> ULongSequence(ulong startValue, ulong incrementBy) {
            return new SequenceValueGenerator<ulong>(startValue, incrementBy);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of floats starting with 0, incrementing by 1.
        /// </summary>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<float> FloatSequence() {
            return new SequenceValueGenerator<float>(0f, 1f);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of floats starting w/ the specified start value, incremented by 1.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<float> FloatSequence(float startValue) {
            return new SequenceValueGenerator<float>(startValue, 1f);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of floats starting w/ the specified start value, incremented by a specified value.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <param name="incrementBy">The value to increment by.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<float> FloatSequence(float startValue, float incrementBy) {
            return new SequenceValueGenerator<float>(startValue, incrementBy);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of doubles starting with 0, incrementing by 1.
        /// </summary>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<double> DoubleSequence() {
            return new SequenceValueGenerator<double>(0d, 1d);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of doubles starting w/ the specified start value, incremented by 1.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<double> DoubleSequence(double startValue) {
            return new SequenceValueGenerator<double>(startValue, 1d);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of doubles starting w/ the specified start value, incremented by a specified value.
        /// </summary>
        /// <param name="startValue">The value to start with.</param>
        /// <param name="incrementBy">The value to increment by.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<double> DoubleSequence(double startValue, double incrementBy) {
            return new SequenceValueGenerator<double>(startValue, incrementBy);
        }

        /// <summary>
        /// Creates a generator which will create a sequence of value which will repeat to the begninning once the end has been reached.
        /// </summary>
        /// <typeparam name="T">The type of value the generator will return.</typeparam>
        /// <param name="values">The values which define the repeating sequence.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<T> RepeatableSequence<T>(IEnumerable<T> values) {
            return new RepeatingSequence<T>(values);
        }

        #endregion

        /// <summary>
        /// Creates a generator which will return values randomly chosen from a collection.
        /// </summary>
        /// <typeparam name="T">The type of value the generator will return.</typeparam>
        /// <param name="sourceCollection">The source collection which will be randomly picked from.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<T> FromSelector<T>(ICollection<T> sourceCollection) {
            return new Selector<T>(sourceCollection);
        }

        /// <summary>
        /// Creates a Selector for string values by reading the values of a text file.
        /// 
        /// The text file is assumed have one value per line.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>A new Selector for string values defined by the given file.</returns>
        public static IValueGenerator<string> FromSelector(String filePath) {
            return new Selector<string>(FileUtils.ReadValuesFromTextFile(File.OpenText(filePath), new Func<string, string>(x => x)));
        }

        /// <summary>
        ///  Creates a generator which will return values randomly chosen from a collection, based upon weights assigned to each value in the collection.
        /// </summary>
        /// <typeparam name="T">The type of value the generator will return.</typeparam>
        /// <param name="sourceCollection">The collection containing the values and the associated weights.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<T> FromWeightedSelector<T>(IEnumerable<KeyValuePair<T,int>> sourceCollection) {
            return new WeightedSelector<T>(sourceCollection);
        }

        private static IValueGenerator<string> WeightedSelectorFromEmbeddedResource(string resource) {
            TextReader reader = new StreamReader(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(resource));
            return new WeightedSelector<string>(FileUtils.ReadWeightedValuesFromTextFile(reader, FileUtils.WeightedValueTSVLineParser));
        }

        /// <summary>
        /// Creates a Weigthed Selector for string values by reading the values of a text file.
        /// 
        /// The text file is assumed have one value/weight combination on each line, seperated by a tab character.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>A new Weighted Selector for string values defined by the given file.</returns>
        public static IValueGenerator<string> FromWeightedSelector(string filePath) {
            return new WeightedSelector<string>(FileUtils.ReadWeightedValuesFromTextFile(File.OpenText(filePath), FileUtils.WeightedValueTSVLineParser));
        }

        /// <summary>
        /// Selects female first names based on a weighted data set provided by the 1990 U.S. census.
        /// </summary>
        /// <see cref="!:https://www.census.gov/genealogy/www/data/1990surnames/names_files.html)"/>
        /// <returns>A string selector for female names.</returns>
        public static IValueGenerator<string> FromFemaleFirstNames() {
            return Generators.WeightedSelectorFromEmbeddedResource("PocoGenerator.ValueGenerators.RandomValueGenerators.Data.FemaleNames.txt");
        }

        /// <summary>
        /// Selects male first names based on a weighted data set provided by the 1990 U.S. census.
        /// </summary>
        /// <see cref="!: https://www.census.gov/genealogy/www/data/1990surnames/names_files.html"/>
        /// <returns>A string selector for male names.</returns>
        public static IValueGenerator<string> FromMaleFirstNames() {
            return Generators.WeightedSelectorFromEmbeddedResource("PocoGenerator.ValueGenerators.RandomValueGenerators.Data.MaleNames.txt");
        }

        /// <summary>
        /// Selects last names based on a weighted data set provided by the 1990 U.S. census
        /// </summary>
        /// <see cref="!:https://www.census.gov/genealogy/www/data/1990surnames/names_files.html"/>
        /// <returns>A string selector for last names.</returns>
        public static IValueGenerator<string> FromLastNames() {
            return Generators.WeightedSelectorFromEmbeddedResource("PocoGenerator.ValueGenerators.RandomValueGenerators.Data.LastNames.txt");
        }


        /// <summary>
        /// Selects U.S. cities based on a weighted data set provided by the 2010 U.S. census
        /// </summary>
        /// <see cref="!:http://www.census.gov/popest/data/cities/totals/2011/index.html"/>
        /// <returns>A string selector for last names.</returns>
        public static IValueGenerator<string> FromUSCities() {
            return Generators.WeightedSelectorFromEmbeddedResource("PocoGenerator.ValueGenerators.RandomValueGenerators.Data.USCities.txt");
        }

        /// <summary>
        /// Creates a new formatted string generator.
        /// </summary>
        /// <param name="format">The format of the string to be generated.</param>
        /// <param name="parameters">An array of IValueGenerators whose values will be substitued into the formatted string.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<string> FormattedString(string format, params dynamic[] parameters) {
            return new FormattedStringGenerator(format, parameters);
        }

        /// <summary>
        /// Creates a new conditional value generator.
        /// </summary>
        /// <typeparam name="TParentType">The type of the parent object.</typeparam>
        /// <typeparam name="T">The type of the property/memeber this generator will be associated with.</typeparam>
        /// <returns>A new value generator. </returns>
        public static ConditionalValueGenerator<TParentType, T> Conditional<TParentType, T>() {
            return new ConditionalValueGenerator<TParentType, T>();
        }


        /// <summary>
        /// Returns a new instance of FunctionValueGenerator
        /// </summary>
        /// <param name="function">The custom function for creating new values.</param>
        /// <returns>A new value generator.</returns>
        public static FunctionValueGenerator<TParentType, T> FromFunction<TParentType, T>(Func<TParentType,T> function) {
            return new FunctionValueGenerator<TParentType, T>(function);
        }

        /// <summary>
        /// Creates a new value generator which returns random longs from a given uniform distribution.
        /// </summary>
        /// <param name="min">The minimum value for the range to select from.</param>
        /// <param name="max">The maximum value for the range to select from.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<long> FromUniform(long min, long max) {
            return new LongFromUniformGenerator(min, max);
        }

        /// <summary>
        /// Creates a new value generator which returns random integers from a given uniform distribution.
        /// </summary>
        /// <param name="min">The minimum value for the range to select from.</param>
        /// <param name="max">The maximum value for the range to select from.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<int> FromUniform(int min, int max) {
            return new IntFromUniformGenerator(min, max);
        }

        /// <summary>
        /// Creates a new value generator which returns random doubles from a given uniform distribution.
        /// </summary>
        /// <param name="min">The minimum value for the range to select from.</param>
        /// <param name="max">The maximum value for the range to select from.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<double> FromUniform(double min, double max) {
            return new FromUniformGenerator(min, max);
        }

        /// <summary>
        /// Creates a new value generator which returns random dates from a given uniform distribution.
        /// </summary>
        /// <param name="min">The minimum value for the range to select from.</param>
        /// <param name="max">The maximum value for the range to select from.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<DateTime> FromUniform(DateTime min, DateTime max) {
            return new DateFromUniformGenerator(min, max);
        }

        /// <summary>
        /// Creates a new value generator which returns random doubles from a given normal distribution.
        /// </summary>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<double> FromNormal(double mean, double stddev) {
            return new FromNormalGenerator(mean, stddev);
        }

        /// <summary>
        /// Creates a new value generator which returns random integers from a given normal distribution.
        /// </summary>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<int> FromNormal(int mean, int stddev) {
            return new IntFromNormalGenerator(mean, stddev);
        }

        /// <summary>
        /// Creates a new value generator which returns random longs from a given normal distribution.
        /// </summary>
        /// <param name="mean">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<long> FromNormal(long mean, long stddev) {
            return new LongFromNormalGenerator(mean, stddev);
        }

        /// <summary>
        /// Creates a new value generator which returns random dates from a given normal distribution.
        /// </summary>
        /// <param name="meanTime">The mean of the normal distribution.</param>
        /// <param name="stddev">The standard deviation of the normal distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<DateTime> FromNormal(DateTime meanTime, TimeSpan stddev) {
            return new DateFromNormalGenerator(meanTime, stddev);
        }

        /// <summary>
        /// Creates a new value generator which returns random doubles from a given pareto distribution.
        /// </summary>
        /// <param name="scale">The scale parameter of the pareto distribution.</param>
        /// <param name="shape">The shape parameter of the pareto distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<double> FromPareto(double scale, double shape) {
            return new FromParetoGenerator(scale, shape);
        }

        /// <summary>
        /// Creates a new value generator which returns random integers from a given pareto distribution.
        /// </summary>
        /// <param name="scale">The scale parameter of the pareto distribution.</param>
        /// <param name="shape">The shape parameter of the pareto distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<int> IntFromPareto(double scale, double shape) {
            return new IntFromParetoGenerator(scale, shape);
        }

        /// <summary>
        /// Creates a new value generator which returns random longs from a given pareto distribution.
        /// </summary>
        /// <param name="scale">The scale parameter of the pareto distribution.</param>
        /// <param name="shape">The shape parameter of the pareto distribution.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<long> LongFromPareto(double scale, double shape) {
            return new LongFromParetoGenerator(scale, shape);
        }

        /// <summary>
        /// Creates a new value generator for strings of a certain size composed of words from some other generator.
        /// </summary>
        /// <param name="sizeSelector">The generator which determins the size of the string generated.</param>
        /// <param name="wordList">The generator which determines the words within the string generated.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<String> FromWords(IValueGenerator<int> sizeSelector, IValueGenerator<string> wordList) {
            return new StringGenerator(sizeSelector, wordList);
        }

        /// <summary>
        /// Creates a new value generator for strings of a certain size composed of words from some other generator.
        /// </summary>
        /// <param name="size">The size of the strings generated.</param>
        /// <param name="wordList">The generator which determines the words within the string generated.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<String> FromWords(int size, IValueGenerator<string> wordList) {
            return new StringGenerator(size, wordList);
        }

        /// <summary>
        /// Creates a new value generator for strings of a given size composed of random latin words.
        /// </summary>
        /// <param name="sizeSelector">The generator which determins the size of the string generated.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<String> FromLoremIpsum(IValueGenerator<int> sizeSelector) {
            return new StringGenerator(sizeSelector, Generators.getStringSelectorFromResource("PocoGenerator.ValueGenerators.RandomValueGenerators.Data.LatinWords.txt"));
        }

        /// <summary>
        /// Creates a new value generator for strings of a given size composed of random latin words.
        /// </summary>
        /// <param name="size">The size of the string generated.</param>
        /// <returns>A new value generator.</returns>
        public static IValueGenerator<String> FromLoremIpsum(int size) {
            return new StringGenerator(size, Generators.getStringSelectorFromResource("PocoGenerator.ValueGenerators.RandomValueGenerators.Data.LatinWords.txt"));
        }

        private static IValueGenerator<string> getStringSelectorFromResource(string resource) {
            TextReader reader = new StreamReader(
                 Assembly.GetExecutingAssembly().GetManifestResourceStream(resource));
            return new Selector<string>(FileUtils.ReadValuesFromTextFile(reader, new Func<string, string>(x => x)));
        }
    }
}
