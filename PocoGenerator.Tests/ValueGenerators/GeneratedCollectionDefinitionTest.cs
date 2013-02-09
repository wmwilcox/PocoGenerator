using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using PocoGenerator.ValueGenerators.Literals;
using PocoGenerator.ValueGenerators.RandomValueGenerators;
using PocoGenerator.ValueGenerators.Sequences;
using System;
using System.Collections.Generic;

namespace PocoGenerator.Tests
{
    [TestFixture]
    public class CollectionValueGeneratorTest
    {
        [Test]
        public void TestGeneratedCollectionDefinition_ResultsDifferentWithDifferentSeeds() {
            var makeCount = 25;
            var rnd1 = new Random(911);
            var rnd2 = new Random(666);
            var valGen1 = new Selector<string>(new List<string>(){"a","b","c","d","e","f","g"});
            var valGen2 = new Selector<string>(new List<string>() { "a", "b", "c", "d", "e", "f", "g" });

            var gen1 = new PocoGenerator<Person>(rnd1)
                .WithGeneratedCollection(1, valGen1).Do((person, name) => person.FirstName = name);
            var gen2 = new PocoGenerator<Person>(rnd2)
                .WithGeneratedCollection(1, valGen2).Do((person, name) => person.FirstName = name);

            var result1 = gen1.Make(makeCount);
            var result2 = gen2.Make(makeCount);

            var numDifferent = 0;
            for (int i = 0; i < makeCount; i++)
            {
                if (result1[i].FirstName != result2[i].FirstName)
                    numDifferent++;
            }
            Assert.True(numDifferent != 0);
        }

        [Test]
        public void TestGeneratedCollectionDefinition_RepeatableResultsWhenRandomSourceIsSupplied() {
            var makeCount = 25;
            var rnd1 = new Random(552);
            var rnd2 = new Random(552);
            var valGen1 = new Selector<string>(new List<string>() { "a", "b", "c", "d", "e", "f", "g" });
            var valGen2 = new Selector<string>(new List<string>() { "a", "b", "c", "d", "e", "f", "g" });

            var gen1 = new PocoGenerator<Person>(rnd1)
                .WithGeneratedCollection(1, valGen1).Do((person, name) => person.FirstName = name);
            var gen2 = new PocoGenerator<Person>(rnd2)
                .WithGeneratedCollection(1, valGen2).Do((person, name) => person.FirstName = name);

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.FirstName, b.FirstName);
            });
        }

        [Test]
        public void TestGeneratedCollectionDefinition() {
            var sizeGenerator = new LiteralValueGenerator<int>(2);
            var valueGenerator = new LiteralValueGenerator<string>("a");

            var generator = new PocoGenerator<Person>()
                .WithValues(sizeGenerator, valueGenerator).Do((person, alias) => person.KnownAliases.Add(alias));

            assertResultsAreCorrect(generator.MakeOne());
        }

        [Test]
        public void TestGeneratedCollectionDefinition_WithPropertyDefinitions() {
            var sizeGenerator = new LiteralValueGenerator<int>(2);
            var valueGenerator = new LiteralValueGenerator<string>("a");

            var generator = new PocoGenerator<Person>()
                .WithValues(sizeGenerator, valueGenerator).Do((person, alias) => person.KnownAliases.Add(alias))
                .For(x => x.FirstName).Use("firstName");

            var result = generator.MakeOne();

            Assert.AreEqual(2, result.KnownAliases.Count);
            Assert.AreEqual("a", result.KnownAliases[0]);
            Assert.AreEqual("a", result.KnownAliases[1]);
            Assert.AreEqual("firstName", result.FirstName);
        }

        [Test]
        public void TestCollectionValueGenerator_SizeIsLiteral() {
            var valueGenerator = new LiteralValueGenerator<string>("a");

            var generator = new PocoGenerator<Person>()
                .WithGeneratedCollection(2, valueGenerator).Do((person, alias) => person.KnownAliases.Add(alias));

            assertResultsAreCorrect(generator.MakeOne());
        }

        [Test]
        public void TestCollectionValueGenerator_ActionMakesDicitionary() {
            var valueGenerator = new RepeatingSequence<String>(new List<String>() { "1", "2", "3", "4", "5" });
            var action = new Action<Person, string>((person, alias) => person.AliasesById.Add(Int32.Parse(alias), alias));

            var generator = new PocoGenerator<Person>()
                .WithGeneratedCollection(5, valueGenerator).Do(action);

            var result = generator.MakeOne();

            Assert.AreEqual(5, result.AliasesById.Count);
            Assert.AreEqual("1", result.AliasesById[1]);
            Assert.AreEqual("2", result.AliasesById[2]);
            Assert.AreEqual("3", result.AliasesById[3]);
            Assert.AreEqual("4", result.AliasesById[4]);
            Assert.AreEqual("5", result.AliasesById[5]);
        }


        private void assertResultsAreCorrect(Person person) {
            Assert.AreEqual(2, person.KnownAliases.Count);
            Assert.AreEqual("a", person.KnownAliases[0]);
            Assert.AreEqual("a", person.KnownAliases[1]);
        }
    }
}
