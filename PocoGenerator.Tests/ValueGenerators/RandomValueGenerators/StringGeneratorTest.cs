using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using PocoGenerator.ValueGenerators.RandomValueGenerators;
using PocoGenerator.ValueGenerators.Sequences;
using System;
using System.Collections.Generic;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    public class StringGeneratorTest
    {
        private int makeCount = 10;

        [Test]
        public void TestStringGenerator_FixedSize() {
            IValueGenerator<string> wordSelector = new RepeatingSequence<string>
                (new List<String>(){"a"});

            var generator = new PocoGenerator<Person>()
                .For(x => x.Bio).Use(Generators.FromWords(9, wordSelector));

            var result = generator.MakeOne();

            Assert.AreEqual("a a a a a", result.Bio);
        }

        [Test]
        public void TestStringGenerator_NoOutOfBoundsSubstringWhenLengthLessThanZero() {
            IValueGenerator<string> wordSelector = new RepeatingSequence<string>
                 (new List<String>() { "a" });

            var zeroLengthGenerator = new PocoGenerator<Person>()
                .For(x => x.Bio).Use(Generators.FromWords(0, wordSelector));

            var negativeLengthGenerator = new PocoGenerator<Person>()
                .For(x => x.Bio).Use(Generators.FromWords(0, wordSelector));

            Assert.AreEqual("", zeroLengthGenerator.MakeOne().Bio);
            Assert.AreEqual("", negativeLengthGenerator.MakeOne().Bio);
        }

        [Test]
        public void TestStringGenerator_RandomSize_RandomWordSelection() {
            IValueGenerator<string> wordSelector = new Selector<string>
                (new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
            IValueGenerator<int> sizeSelector = Generators.FromUniform(10, 20);

            var generator = new PocoGenerator<Person>()
                .For(x => x.Bio).Use(Generators.FromWords(sizeSelector, wordSelector));

            var result = generator.Make(makeCount);

            foreach (Person p in result)
                Assert.True((p.Bio.Length >= 10) && (p.Bio.Length <= 20));
        }

        [Test]
        public void TestStringGenerator_RandomSize_RandomWordSelection_RepeatableResults() {
            var rndSeed = 12;
            var rnd1 = new Random(rndSeed);
            var rnd2 = new Random(rndSeed);
            IValueGenerator<string> wordSelector1 = new Selector<string>
                (new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
            IValueGenerator<string> wordSelector2 = new Selector<string>
                (new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
            IValueGenerator<int> sizeSelector1 = Generators.FromUniform(10, 20);
            IValueGenerator<int> sizeSelector2 = Generators.FromUniform(10, 20);
          
            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.Bio).Use(Generators.FromWords(sizeSelector1, wordSelector1));
            var gen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.Bio).Use(Generators.FromWords(sizeSelector2, wordSelector2));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) =>
            {
                Assert.AreEqual(a.Bio, b.Bio);
            });

        }
    }
}
